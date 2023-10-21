using Balta.IBGE.Domain.Accounts.Entities;

namespace Balta.IBGE.Domain.Accounts.Repositories;

public interface IUserRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
}
