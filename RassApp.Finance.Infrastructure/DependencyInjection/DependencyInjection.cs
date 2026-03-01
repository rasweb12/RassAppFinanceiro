using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Infrastructure.Persistence.Repositories;
using RassApp.Security.Abstractions;
using RassApp.Security.Services;
using RassApp.SharedKernel.Abstractions.Persistence;

namespace RassApp.Finance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FinanceDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<FinanceDbContext>());

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        // JWT
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}