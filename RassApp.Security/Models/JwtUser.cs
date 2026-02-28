using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Security.Models;

public sealed class JwtUser
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string TenantId { get; init; } = string.Empty;
}
