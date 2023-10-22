using Balta.IBGE.Domain.Accounts.Entities;
using Balta.IBGE.Domain.Accounts.Repositories;
using Balta.IBGE.Infra.Database;

using Microsoft.EntityFrameworkCore;

namespace Balta.IBGE.Infra.UseCases.Accounts;

public class UserRepository : IUserRepository
{
    private readonly IBGEDbContext _dbContext;

    public UserRepository(IBGEDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
        => await _dbContext
            .Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.Address == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
        => await _dbContext.Users.AddAsync(user, cancellationToken);
}
