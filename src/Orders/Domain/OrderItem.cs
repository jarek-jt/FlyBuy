using Orders.Core;

namespace Orders.Domain;

public class OrderItem : Entity
{
    public OrderItem(Guid productId, int quantity)
    {
        if(Quantity < 0)
            throw new ArgumentException("Quantity must be greater than zero");
        
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid ProductId { get; init; }
    public int Quantity { get; private set; }

    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
    }
    
    public void DecreaseQuantity(int amount)
    {
        if(Quantity - amount < 0)
            throw new ArgumentException("Cannot decrease the quantity below zero");
        
        Quantity -= amount;
    }
}