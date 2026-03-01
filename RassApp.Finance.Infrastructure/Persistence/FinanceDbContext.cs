using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RassApp.Finance.Domain.Common;
using RassApp.Finance.Domain.Entities;
using RassApp.MultiTenancy.Contexts;
using System.Linq.Expressions;
using System.Reflection;
using RassApp.SharedKernel.Abstractions.Persistence;

public class FinanceDbContext : DbContext, IUnitOfWork
{
    private readonly ITenantContext _tenantContext;

    public FinanceDbContext(
        DbContextOptions<FinanceDbContext> options,
        ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext
            ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    private string CurrentTenantId => _tenantContext.TenantId;

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(et => typeof(BaseEntity).IsAssignableFrom(et.ClrType)))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");

            var isDeletedProperty = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new[] { typeof(bool) },
                parameter,
                Expression.Constant(nameof(BaseEntity.IsDeleted)));

            var tenantProperty = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new[] { typeof(string) },
                parameter,
                Expression.Constant(nameof(BaseEntity.TenantId)));

            var isDeletedCondition = Expression.Equal(isDeletedProperty, Expression.Constant(false));
            var tenantCondition = Expression.Equal(tenantProperty, Expression.Constant(CurrentTenantId));

            var combined = Expression.AndAlso(isDeletedCondition, tenantCondition);

            var lambda = Expression.Lambda(combined, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var tenantId = CurrentTenantId;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreated();
                    entry.Entity.SetTenant(tenantId);
                    break;

                case EntityState.Modified:
                    entry.Entity.SetUpdated();
                    break;

                case EntityState.Deleted:
                    entry.Entity.MarkAsDeleted();
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        return await base.SaveChangesAsync(ct);
    }
}