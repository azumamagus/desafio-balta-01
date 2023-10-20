using Balta.IBGE.Domain.Core;

namespace Balta.IBGE.Infra.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly IBGEDbContext _dbContext;

    public UnitOfWork(IBGEDbContext dbContext)
        => _dbContext = dbContext;

    public void BeginTransaction()
        => _dbContext.Database.BeginTransaction();

    public void CommitTransaction()
        => _dbContext.Database.CommitTransaction();

    public void RollbackTransaction()
        => _dbContext.Database.RollbackTransaction();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}