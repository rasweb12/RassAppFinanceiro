using Microsoft.EntityFrameworkCore;
using RassApp.SharedKernel.Abstractions.Persistence;

namespace RassApp.Finance.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly FinanceDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(FinanceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);
}