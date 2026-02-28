using RassApp.MultiTenancy.Contexts;

namespace RassApp.MultiTenancy.Contexts;

public class TenantContext : ITenantContext
{
    private string? _tenantId;

    public string TenantId =>
        _tenantId ?? throw new InvalidOperationException("Tenant não definido.");

    public void SetTenant(string tenantId)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new ArgumentException("Tenant inválido.");

        _tenantId = tenantId;
    }
}