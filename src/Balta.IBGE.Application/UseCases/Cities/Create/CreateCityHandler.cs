using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Create;

internal sealed class CreateCityHandler : IRequestHandler<CreateCityCommand, Result<int>>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCityHandler(
        ICityRepository cityRepository,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = new City(request.Id, request.State, request.Name);

        await _cityRepository.AddAsync(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(city.Id);
    }
}
