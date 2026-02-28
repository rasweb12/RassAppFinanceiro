using Microsoft.EntityFrameworkCore;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Domain.Common;

namespace RassApp.Finance.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly FinanceDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(FinanceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await _dbSet.AddAsync(entity, ct);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);
}