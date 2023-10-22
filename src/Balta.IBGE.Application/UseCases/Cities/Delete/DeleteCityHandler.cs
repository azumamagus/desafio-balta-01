using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Application.UseCases.Cities.Create;
using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;
using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Delete
{
    internal sealed class DeleteCityHandler : IRequestHandler<DeleteCityCommand, Result>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.id);

            if (city is null)
                return Result.Failure("City not found");

            await _cityRepository.DeleteAsync(city!);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
