namespace Basket.API.Models;

public class ShppingCartItem
{
    public Guid ProductId { get; set; } = default(Guid);
    public string ProductName { get; set; } = default!;
    public string Color { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}

