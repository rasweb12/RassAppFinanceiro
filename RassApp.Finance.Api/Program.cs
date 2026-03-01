using Asp.Versioning;
using Microsoft.OpenApi.Models;
using RassApp.Finance.Api.Extensions;
using RassApp.Finance.Api.HealthChecks;
using RassApp.Finance.Application.DependencyInjection;
using RassApp.Finance.Infrastructure;
using RassApp.MultiTenancy;
using RassApp.Security;

var builder = WebApplication.CreateBuilder(args);

// ================================================
// 1. REGISTRO DE SERVIÇOS
// ================================================

// 1️⃣ Multi-Tenancy
builder.Services.AddMultiTenancy();

// 2️⃣ Application
builder.Services.AddApplicationLayer();

// 3️⃣ Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// 4️⃣ Security (JWT)
builder.Services.AddSecurity(builder.Configuration);

// 5️⃣ API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// 6️⃣ Controllers
builder.Services.AddControllers();

// 7️⃣ Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinancasApp API",
        Version = "v1",
        Description = "API de controle financeiro pessoal com autenticação JWT"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ================================================
// 2. BUILD
// ================================================

var app = builder.Build();

// ================================================
// 3. PIPELINE
// ================================================

// 1️⃣ Exception Handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancasApp API v1");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionMiddleware();
}

// 2️⃣ HTTPS
app.UseHttpsRedirection();

// 3️⃣ Routing
app.UseRouting();

// 4️⃣ Multi-Tenant (ANTES da autenticação)
app.UseTenantMiddleware();

// 5️⃣ Authentication
app.UseAuthentication();

// 6️⃣ Authorization
app.UseAuthorization();

// 7️⃣ Endpoints
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();