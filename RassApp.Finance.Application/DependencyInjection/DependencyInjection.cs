using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RassApp.Finance.Application.Behaviors;
using System.Reflection;

namespace RassApp.Finance.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}