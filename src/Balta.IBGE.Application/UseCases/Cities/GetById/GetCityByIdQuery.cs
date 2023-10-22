using Balta.IBGE.Application.UseCases.Cities.ViewModels;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.GetById;

public record GetCityByIdQuery(
    int Id) : IRequest<Result<CityViewModel>>;