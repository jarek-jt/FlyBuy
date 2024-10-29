namespace Orders.Events;

public class OrderFinalized : Event
{
    public required Guid OrderId { get; set; }
    public override Guid StreamId => OrderId;
    
    public static OrderFinalized Create(Guid orderId)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException(nameof(orderId));
        
        return new OrderFinalized()
        {
            OrderId = orderId
        };
    }
}