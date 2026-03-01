using Microsoft.EntityFrameworkCore;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Domain.Entities;
using RassApp.SharedKernel.Abstractions.Persistence;

namespace RassApp.Finance.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FinanceDbContext _context;

    public UserRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}