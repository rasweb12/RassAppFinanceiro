using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.SharedKernel.Abstractions.Persistence;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
