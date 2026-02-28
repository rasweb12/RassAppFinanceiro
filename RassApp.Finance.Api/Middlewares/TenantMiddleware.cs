using RassApp.MultiTenancy.Contexts;

namespace RassApp.Finance.Api.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        string? tenantId = null;

        if (context.User.Identity?.IsAuthenticated == true)
            tenantId = context.User.FindFirst("tenant")?.Value;

        if (string.IsNullOrWhiteSpace(tenantId))
            tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(tenantId))
            throw new UnauthorizedAccessException("Tenant não informado.");

        tenantContext.SetTenant(tenantId);

        await _next(context);
    }
}