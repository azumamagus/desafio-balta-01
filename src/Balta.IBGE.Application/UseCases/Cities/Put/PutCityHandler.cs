using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Application.UseCases.Cities.ViewModels;
using Balta.IBGE.Domain.Cities.Repositories;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Put
{
    internal sealed class PutCityHandler : IRequestHandler<PutCityCommand, Result>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PutCityHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(PutCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.Id, cancellationToken);

            if (city is null)
                return Result.Failure("City not found");

            city.UpdateCity(request.Id, request.State, request.Name);

            _cityRepository.Update(city);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
