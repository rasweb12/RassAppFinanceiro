namespace RassApp.MultiTenancy.Contexts;

public interface ITenantContext
{
    string TenantId { get; }
    void SetTenant(string tenantId);
}