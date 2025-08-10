﻿using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; } = default!;
    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be an empty GUID.");
        }
        return new OrderId(value);
    }
}
