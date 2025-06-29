using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; } = default;
    public string OrderName { get; private set; } = string.Empty;
    public Address ShippingAddress { get; private set; } = new();
    public Address BillingAddress { get; private set; } = new();
    public Payment PaymentDetails { get; private set; } = new();
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalAmount
    {
        get => _orderItems.Sum(item => item.Price * item.Quantity);
        private set { } // This setter is intentionally left empty to prevent external modification
    }

}
