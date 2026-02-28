using RassApp.Finance.Domain.Entities;

namespace RassApp.Finance.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    IUnitOfWork UnitOfWork { get; }
}
