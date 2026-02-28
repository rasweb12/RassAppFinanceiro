using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RassApp.MultiTenancy.Contexts;

namespace RassApp.MultiTenancy;

public static class DependencyInjection
{
    public static IServiceCollection AddMultiTenancy(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITenantContext, TenantContext>();
        return services;
    }
}