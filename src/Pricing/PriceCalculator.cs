namespace Pricing;

public class PriceCalculator
{
    public decimal GetPrice(List<string> products)
    {
        if(products.Count == 0)
            throw new ArgumentException("You must provide at least one product.");

        var price = products.Sum(str => str.GetHashCode());
        price /= products.Count;
        
        return decimal.Round(price, 2);
    }
}