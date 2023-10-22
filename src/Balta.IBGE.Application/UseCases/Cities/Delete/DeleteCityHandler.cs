using Balta.IBGE.Domain.Cities.Repositories;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Delete;

internal sealed class DeleteCityHandler : IRequestHandler<DeleteCityCommand, Result>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCityHandler(
        ICityRepository cityRepository,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id, cancellationToken);

        if (city is null)
            return Result.Failure("City not found");

        _cityRepository.Remove(city!);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
