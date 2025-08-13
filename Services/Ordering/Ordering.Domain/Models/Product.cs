using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string ProductName { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Product Create(ProductId id, string productName, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfLessThan(price, 0);
        return new Product
        {
            Id = id,
            ProductName = productName,
            Price = price
        };
    }
}
