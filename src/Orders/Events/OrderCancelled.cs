namespace Orders.Events;

public class OrderCancelled : Event
{
    public required Guid OrderId { get; set; }
    public override Guid StreamId => OrderId;
    
    public static OrderCancelled Create(Guid orderId)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException(nameof(orderId));
        
        return new OrderCancelled()
        {
            OrderId = orderId
        };
    }
}