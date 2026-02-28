using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RassApp.MultiTenancy.Contexts;

namespace RassApp.Finance.Infrastructure.Persistence;

public class FinanceDbContextFactory
    : IDesignTimeDbContextFactory<FinanceDbContext>
{
    public FinanceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=RassAppFinanceDb;User Id=rass_adm;Password=Nucleo@001;TrustServerCertificate=True;");

        var tenantContext = new DesignTimeTenantContext();

        return new FinanceDbContext(optionsBuilder.Options, tenantContext);
    }
}

internal class DesignTimeTenantContext : ITenantContext
{
    private string _tenantId = "design-time-tenant";

    public string TenantId => _tenantId;

    public void SetTenant(string tenantId)
    {
        _tenantId = tenantId;
    }
}