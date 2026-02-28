using System;
using System.Collections.Generic;
using System.Text;
using RassApp.Finance.Domain.Common;

namespace RassApp.Finance.Domain.Entities;

public class Account : BaseEntity
{
    public string Name { get; private set; }

    protected Account() { }

    public Account(string name)
    {
        Name = name;
        SetCreated();
    }
}
