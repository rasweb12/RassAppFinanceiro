using RassApp.Finance.Domain.Entities;
using RassApp.SharedKernel.Abstractions.Persistence;

namespace RassApp.Finance.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    IUnitOfWork UnitOfWork { get; }
}