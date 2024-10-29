using System.Collections.Concurrent;
using Orders.Core;
using Orders.Domain;

namespace Orders;

/// <summary>
/// In real system the projection would be taken from EventStoreDb
/// </summary>
public class OrderProjection
{
    private static readonly ConcurrentDictionary<Guid, Aggregate> _orders = new ();
    
    internal void UpdateProjections(Aggregate order)
    {
        _orders.AddOrUpdate(order.Id, order, (_, _) => order);
    }
    
    public Order? GetOrder(Guid orderId)
    {
        return _orders.GetValueOrDefault(orderId) as Order;
    }
    
    public List<Order> GetCustomerOrdersForMonth(Guid customerId, short month, short year)
    {
        var orders = _orders
            .Select(x => x.Value as Order)
            .Where(order => order != null
                            && order.OrderedDate.Year == year
                            && order.OrderedDate.Month == month
                            && order.CustomerId == customerId 
                            && order.Status == OrderStatus.Finalized);
        
        return orders.ToList();
    }
}