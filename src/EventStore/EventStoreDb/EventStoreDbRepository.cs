using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using EventStore.Client;

namespace EventStore.EventStoreDb;

public class EventStoreDbRepository : IEventStoreRepository
{
    private readonly EventStoreClient _eventStoreClient;
    private readonly JsonSerializerOptions _options = new() {TypeInfoResolver = new DefaultJsonTypeInfoResolver() };

    public EventStoreDbRepository(EventStoreClient eventStoreClient)
    {
        _eventStoreClient = eventStoreClient;
    }
    
    public async Task<long> AppendAsync<T>(byte[] @event, byte[] metadata, string streamId)
    {
        var eventData = new EventData(
            Uuid.NewUuid(),
            typeof(T).Name,
            @event.AsMemory(),
            metadata.AsMemory());

        var result = await _eventStoreClient.AppendToStreamAsync(
            streamId,
            StreamState.Any, 
            new List<EventData>
            {
                eventData
            });
        
        return result.NextExpectedStreamRevision.ToInt64();
    }

    public async Task<List<T>> GetAsync<T>(Guid id)
    {
        var events = _eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            id.ToString(),
            StreamPosition.Start
        );
        
        var deserializedEvents = new List<T>();

        await foreach (var jsonEvent in events)
        {
            var @event = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(jsonEvent.Event.Data.ToArray()), _options);
            
            if(@event == null)
                continue;
            
            deserializedEvents.Add(@event);
        }

        return deserializedEvents;
    }
}