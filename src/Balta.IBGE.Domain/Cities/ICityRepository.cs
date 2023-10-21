namespace Balta.IBGE.Domain.Cities;

public interface ICityRepository
{
    Task AddAsync(City city);
    Task<List<City>> GetAllAsync();
}
