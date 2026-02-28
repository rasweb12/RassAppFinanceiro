using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Infrastructure.Persistence;
using RassApp.Finance.Infrastructure.Persistence.Repositories;

namespace RassApp.Finance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FinanceDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default")));

        // DbContext como UnitOfWork
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<FinanceDbContext>());

        // Repository genérico
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Repositories específicos
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}