using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment PaymentDetails { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalAmount
    {
        get => _orderItems.Sum(item => item.Price * item.Quantity);
        private set { } // This setter is intentionally left empty to prevent external modification
    }

    public static Order Create(
        OrderId id,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment paymentDetails)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(paymentDetails);

        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            PaymentDetails = paymentDetails
        };

        return order;
    }

    public void Update(
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment paymentDetails,
        OrderStatus status)
    {
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(paymentDetails);
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        PaymentDetails = paymentDetails;
        Status = status;
    }

    public void AddOrderItem(ProductId productId, int quantity, decimal price)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(price, 0);

        if (_orderItems.Any(item => item.ProductId.Equals(productId)))
        {
            throw new InvalidOperationException("Product already exists in the order.");
        }

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(ProductId productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId.Equals(productId));
        if (orderItem == null)
        {
            throw new InvalidOperationException("Order item not found.");
        }
        _orderItems.Remove(orderItem);
    }

}
