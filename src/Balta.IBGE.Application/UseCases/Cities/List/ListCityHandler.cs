using System.Linq.Expressions;

using Balta.IBGE.Application.Extensions;
using Balta.IBGE.Domain.Cities.Entities;
using Balta.IBGE.Domain.Cities.Repositories;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get
{
    public class ListCityHandler : IRequestHandler<ListCityQuery, Result<IEnumerable<CityViewModel>>>
    {
        private readonly ICityRepository _cityRepository;
        
        public ListCityHandler(
            ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Result<IEnumerable<CityViewModel>>> Handle(ListCityQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<City, bool>> predicate = c => true;
            predicate = predicate.Combine(request.FilterOptions.HasId(), Expression.And, c => c.Id == request.FilterOptions.Id);
            predicate = predicate.Combine(request.FilterOptions.HasState(), Expression.And, c => c.State == request.FilterOptions.State);
            predicate = predicate.Combine(request.FilterOptions.HasName(), Expression.And, c => c.Name.Contains(request.FilterOptions.Name!));

            var cityList = await _cityRepository.ListAsync(
                predicate,
                request.FilterOptions.Page,
                request.FilterOptions.PageSize,
                cancellationToken);

            return Result.Success(
                cityList
                .Select(city => new CityViewModel(
                    city.Id,
                    city.State,
                    city.Name)));
        }
    }
}
