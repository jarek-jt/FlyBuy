namespace Orders.Events;

public class ProductAdded : Event
{
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
    public override Guid StreamId => OrderId;
}