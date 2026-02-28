using RassApp.Finance.Api.Middlewares;
using RassAppFinanceiro.RassApp.Finance.Api.Middlewares;

namespace RassApp.Finance.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }

    public static IApplicationBuilder UseTenantMiddleware(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<TenantMiddleware>();
    }
}