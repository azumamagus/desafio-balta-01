using Balta.IBGE.Application.UseCases.Cities.ViewModels;
using Balta.IBGE.Domain.Cities.Repositories;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.GetById;

internal sealed class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, Result<CityViewModel>>
{
    private readonly ICityRepository _cityRepository;
    
    public GetCityByIdHandler(ICityRepository cityRepository)
        => _cityRepository = cityRepository;
    
    public async Task<Result<CityViewModel>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id, cancellationToken);

        if (city is null)
            return Result.Failure<CityViewModel>("City not found");

        var cityViewModel = new CityViewModel(
            city!.Id,
            city!.State,
            city!.Name);

        return Result.Success(cityViewModel);
    }
}
