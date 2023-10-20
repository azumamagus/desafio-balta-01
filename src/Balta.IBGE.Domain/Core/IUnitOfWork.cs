namespace Balta.IBGE.Domain.Core;

public  interface IUnitOfWork
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
