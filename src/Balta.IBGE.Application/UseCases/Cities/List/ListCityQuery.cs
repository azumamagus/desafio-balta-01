using Balta.IBGE.Application.UseCases.Cities.ViewModels;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.List;

public record ListCityQuery(
    ListCityOptions FilterOptions) : IRequest<Result<IEnumerable<CityViewModel>>>;