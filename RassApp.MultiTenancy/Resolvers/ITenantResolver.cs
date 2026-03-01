using Microsoft.AspNetCore.Http;
namespace RassApp.MultiTenancy.Resolvers;


public interface ITenantResolver
{
    string Resolve();
}