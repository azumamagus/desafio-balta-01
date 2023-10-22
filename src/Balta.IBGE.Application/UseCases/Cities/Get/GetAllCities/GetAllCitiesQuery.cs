using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using FluentValidation;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get.GetAllCities;

public class GetAllCitiesQuery : IRequest<Result<IEnumerable<CityViewModel>>>
{
}