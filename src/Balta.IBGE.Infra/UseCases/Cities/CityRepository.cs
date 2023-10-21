using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Infra.Database;

namespace Balta.IBGE.Infra.UseCases.Cities;

public class CityRepository : ICityRepository
{
    private readonly IBGEDbContext _dbContext;

    public CityRepository(IBGEDbContext dbContext)
        => _dbContext = dbContext;

    public async Task AddAsync(City city, CancellationToken cancellationToken)
        => await _dbContext.Cities.AddAsync(city, cancellationToken);
}