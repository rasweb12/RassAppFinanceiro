using System;
using System.Collections.Generic;
using System.Text;
using RassApp.Finance.Domain.Common;

namespace RassApp.Finance.Application.Abstractions;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task AddAsync(TEntity entity, CancellationToken ct = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
