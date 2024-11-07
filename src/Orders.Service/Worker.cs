using EventStore;
using MyEventStore;
using Orders.Domain;
using Orders.Events;

namespace Orders.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEventStoreRepository _eventStore;
    private readonly OrderProjection _orderProjection;

    public Worker(ILogger<Worker> logger, IEventStoreRepository eventStore, OrderProjection orderProjection)
    {
        _logger = logger;
        _eventStore = eventStore;
        _orderProjection = orderProjection;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        var store = new OrderRepository(_eventStore, _orderProjection);


        var orderId = Guid.NewGuid();
        
        var testEvent = new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = Guid.NewGuid(),
            Created = DateTime.Now
        };
        
        var testEvent2 = new ProductAdded()
        {
            OrderId = orderId,
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            Created = DateTime.Now
        };
        
        var testEvent3 = new OrderFinalized()
        {
            OrderId = orderId,
            Created = DateTime.Now
        };
        
        
        await store.AppendAsync<Order, OrderCreated>(testEvent);
        await store.AppendAsync<Order, ProductAdded>(testEvent2);
        await store.AppendAsync<Order, OrderFinalized>(testEvent3);
        
        var order = await store.GetAsync<Order>(orderId);
        
        Console.WriteLine(order);
                
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}