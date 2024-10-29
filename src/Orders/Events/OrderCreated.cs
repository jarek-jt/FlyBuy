namespace Orders.Events;

public class OrderCreated : Event
{
    public required Guid OrderId { get; init; }
    public required Guid CustomerId { get; init; }
    public override Guid StreamId => OrderId;

    public static OrderCreated Create(Guid orderId, Guid customerId)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException(nameof(orderId));
        if (customerId == Guid.Empty)
            throw new ArgumentException(nameof(customerId));
        
        return new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = customerId
        };
    }
}