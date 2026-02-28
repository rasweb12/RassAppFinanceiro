using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.SharedKernel.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
