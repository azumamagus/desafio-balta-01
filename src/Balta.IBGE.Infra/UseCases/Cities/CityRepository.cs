using System.Linq.Expressions;

using Balta.IBGE.Domain.Cities.Entities;
using Balta.IBGE.Domain.Cities.Repositories;
using Balta.IBGE.Infra.Database;

using Microsoft.EntityFrameworkCore;

namespace Balta.IBGE.Infra.UseCases.Cities;

public class CityRepository : ICityRepository
{
    private readonly IBGEDbContext _dbContext;

    public CityRepository(IBGEDbContext dbContext)
        => _dbContext = dbContext;

    public async Task AddAsync(City city, CancellationToken cancellationToken)
        => await _dbContext.Cities.AddAsync(city, cancellationToken);

    public async Task<IEnumerable<City>> ListAsync(
        Expression<Func<City, bool>> predicate,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
        => await _dbContext.Cities
            .Where(predicate)
            .Skip(pageNumber == 1 ? 0 : (pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    
    public async Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.Cities.FindAsync(id, cancellationToken);
    
    public void Remove(City city) 
        => _dbContext.Cities.Remove(city);
}