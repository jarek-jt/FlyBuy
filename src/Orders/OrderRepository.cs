using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using EventStore;
using Microsoft.Extensions.Logging.Abstractions;
using Orders.Core;
using Orders.Domain;
using Orders.Events;

namespace Orders;

public class OrderRepository(IEventStoreRepository eventStore, OrderProjection projection)
{
    
    private const string StreamName = "Orders";
    private readonly JsonSerializerOptions _options = new() {TypeInfoResolver = new DefaultJsonTypeInfoResolver() };
    
    
    public async Task<long> AppendAsync<TAgg, T>(T orderEvent) where T: Event where TAgg: Aggregate, new()
    {
        try
        {
            var serializedEvent = JsonSerializer.SerializeToUtf8Bytes<Event>(orderEvent, _options);
            var serializedEventMetadata = JsonSerializer.SerializeToUtf8Bytes(StreamName);

            var nextVersion = await eventStore.AppendAsync<T>(
                serializedEvent,
                serializedEventMetadata,
                orderEvent.StreamId.ToString());

            if (nextVersion == 0)
            {
                var aggregate = await GetAsync<TAgg>(orderEvent.StreamId);
                projection.UpdateProjections(aggregate);
            }
            
            return nextVersion;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<T> GetAsync<T>(Guid orderId) where T : Aggregate, new()
    {
        try
        {
            var events = await eventStore.GetAsync<Event>(orderId);

            var order = new T();

            foreach (var orderEvent in events)
            {
                order.Apply(orderEvent);
            }

            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}