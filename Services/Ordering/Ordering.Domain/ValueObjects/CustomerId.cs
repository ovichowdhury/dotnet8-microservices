﻿using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; } = default!;
    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot be an empty GUID.");
        }
        return new CustomerId(value);
    }
}
