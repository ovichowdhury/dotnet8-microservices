﻿using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string ProductName { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
