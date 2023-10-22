namespace Balta.IBGE.Domain.Cities;

public interface ICityRepository
{
    Task AddAsync(City city, CancellationToken cancellationToken);
    Task<IEnumerable<City>> GetAllAsync();
    Task<City?> GetByIdAsync(int id);
    Task DeleteAsync(City city);
}
