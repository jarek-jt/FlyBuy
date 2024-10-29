using Orders.Core;
using Orders.Events;

namespace Orders.Domain;

public class Order : Aggregate
{
    public Guid CustomerId { get; set; }
    public Dictionary<Guid, OrderItem> Products { get; set; } = new();
    public OrderStatus Status { get; set; }

    public Price TotalPrice { get; set; } = new (0);
    
    public DateTime OrderedDate { get; set; }

    private void Apply(OrderCreated orderCreated)
    {
        Id = orderCreated.OrderId;
        CustomerId = orderCreated.CustomerId;
        Status = OrderStatus.InProgress;
    }
    
    private void Apply(OrderCancelled orderCancelled)
    {
        Id = orderCancelled.OrderId;
        Status = OrderStatus.Canceled;
    }
    
    private void Apply(OrderFinalized orderFinalized)
    {
        Id = orderFinalized.OrderId;
        Status = OrderStatus.Finalized;
        OrderedDate = DateTime.Now;
    }
    
    private void Apply(ProductAdded productAdded)
    {
        Id = productAdded.OrderId;
        
        if (Products.ContainsKey(productAdded.ProductId))
        {
            Products[productAdded.ProductId].IncreaseQuantity(productAdded.Quantity);
        }
        else
        {
            Products.Add(productAdded.ProductId, new OrderItem(productAdded.ProductId, productAdded.Quantity));
        }
    }
    
    private void Apply(ProductRemoved productRemoved)
    {
        Id = productRemoved.OrderId;
        
        if (Products.ContainsKey(productRemoved.ProductId))
        {
            Products[productRemoved.ProductId].DecreaseQuantity(productRemoved.Quantity);
        }
    }

    public override void Apply(Event orderEvent)
    {
        switch (orderEvent)
        {
            case OrderCreated orderCreated:
                Apply(orderCreated);
                break;
            case OrderCancelled orderCancelled:
                Apply(orderCancelled);
                break;
            case OrderFinalized orderFinalized:
                Apply(orderFinalized);
                break;
            case ProductAdded productAdded:
                Apply(productAdded);
                break;
            case ProductRemoved productRemoved:
                Apply(productRemoved);
                break;
        }
    }
}