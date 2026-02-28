using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Domain.Common;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id;
    }
}