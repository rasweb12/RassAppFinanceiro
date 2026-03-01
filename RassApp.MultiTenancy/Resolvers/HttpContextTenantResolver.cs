using Microsoft.AspNetCore.Http;

namespace RassApp.MultiTenancy.Resolvers;

public class HttpContextTenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _accessor;

    public HttpContextTenantResolver(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Resolve()
    {
        var context = _accessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext não disponível.");

        // 1️⃣ Claim (prioridade máxima)
        var tenantId = context.User?.FindFirst("tenant_id")?.Value;

        // 2️⃣ Header fallback
        if (string.IsNullOrWhiteSpace(tenantId))
        {
            tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        }

        if (string.IsNullOrWhiteSpace(tenantId))
            throw new InvalidOperationException("TenantId não encontrado na requisição.");

        return tenantId;
    }
}