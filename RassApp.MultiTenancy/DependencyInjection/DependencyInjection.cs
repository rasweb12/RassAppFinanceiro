using Microsoft.Extensions.DependencyInjection;
using RassApp.MultiTenancy.Contexts;
using RassApp.MultiTenancy.Resolvers;

namespace RassApp.MultiTenancy;

public static class DependencyInjection
{
    public static IServiceCollection AddMultiTenancy(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ITenantContext, TenantContext>();
        services.AddScoped<ITenantResolver, HttpContextTenantResolver>();

        return services;
    }
}