﻿using System.Linq.Expressions;

using Balta.IBGE.Domain.Cities.Entities;

namespace Balta.IBGE.Domain.Cities.Repositories;

public interface ICityRepository
{
    Task AddAsync(City city, CancellationToken cancellationToken);
    Task<IEnumerable<City>> ListAsync(
        Expression<Func<City, bool>> predicate,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
    Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken);
    void Remove(City city);
    void Update(City city);
}
