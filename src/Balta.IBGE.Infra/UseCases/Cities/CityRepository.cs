using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Infra.Database;

using Microsoft.EntityFrameworkCore;

namespace Balta.IBGE.Infra.UseCases.Cities;

public class CityRepository : ICityRepository
{
    private readonly IBGEDbContext _dbContext;

    public CityRepository(IBGEDbContext dbContext)
        => _dbContext = dbContext;
    public async Task AddAsync(City city)
        => await _dbContext.AddAsync(city);
    public async Task<IEnumerable<City>> GetAllAsync() 
        => await _dbContext.Cities.ToListAsync();
    public async Task<City?> GetByIdAsync(int id)
        => await _dbContext.Cities.FindAsync(id);
    public async Task DeleteAsync(City city) 
        => _dbContext.Cities.Remove(city);
}