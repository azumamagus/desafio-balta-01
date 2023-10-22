using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get;

public record ListCityQuery(
    ListCityOptions FilterOptions) : IRequest<Result<IEnumerable<CityViewModel>>>;