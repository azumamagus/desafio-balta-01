using AutoMapper;

using Balta.IBGE.Domain.Cities;

using MediatR;

namespace Balta.IBGE.Application;

public class CreateCityHandler : IRequestHandler<CreateCityRequest, CreateCityResponse>
{
    private readonly IMapper _mapper;

    public async Task<CreateCityResponse> Handle(CreateCityRequest request,
        CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        _cityRepository.Create(city);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CreateCityResponse>(city);
    }
}