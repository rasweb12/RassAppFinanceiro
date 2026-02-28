namespace RassApp.Finance.Api.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddHealthChecks();

        return services;
    }
}