using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot() { }

    protected AggregateRoot(TId id) : base(id) { }
}
