using Orders.Core;

namespace Orders.Domain;

public record Price : ValueObject
{
    public Price(decimal amount)
    {
        if(amount < 0)
            throw new ArgumentException("Price cannot be negative", nameof(amount));
        
        Amount = amount;
    }

    public decimal Amount { get; init; }
    public string Currency => "USD";
}