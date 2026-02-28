using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Application.Features.Accounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
