using Microsoft.EntityFrameworkCore;
using RassApp.Finance.Domain.Common;
using RassApp.Finance.Domain.Entities;
using RassApp.MultiTenancy.Contexts;
using RassApp.Finance.Application.Abstractions;
using System.Reflection;

namespace RassApp.Finance.Infrastructure.Persistence;

public class FinanceDbContext : DbContext, IUnitOfWork
{
    private readonly ITenantContext _tenantContext;

    // ✅ Propriedade usada pelo EF no filtro global
    public string TenantId => _tenantContext.TenantId;

    public FinanceDbContext(
        DbContextOptions<FinanceDbContext> options,
        ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>(); // ⚠ importante se estiver usando

    private void SetGlobalQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : BaseEntity
    {
        builder.Entity<TEntity>()
            .HasQueryFilter(e =>
                !e.IsDeleted &&
                e.TenantId == TenantId);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FinanceDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(FinanceDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter),
                        BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreated();
                    entry.Entity.SetTenant(TenantId);
                    break;

                case EntityState.Modified:
                    entry.Entity.SetUpdated();
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.MarkAsDeleted();
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}