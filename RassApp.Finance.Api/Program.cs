using Asp.Versioning;
using RassApp.Finance.Api.Configuration;
using RassApp.Finance.Api.Extensions;
using RassApp.Finance.Api.HealthChecks;
using RassApp.Finance.Application.DependencyInjection;
using RassApp.Finance.Infrastructure;
using RassApp.MultiTenancy;
using RassApp.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiConfiguration(builder.Configuration)
    .AddApplicationLayer()
    .AddInfrastructure(builder.Configuration)
    .AddSecurity(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});


var app = builder.Build();

app.UseExceptionMiddleware();
app.UseTenantMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();