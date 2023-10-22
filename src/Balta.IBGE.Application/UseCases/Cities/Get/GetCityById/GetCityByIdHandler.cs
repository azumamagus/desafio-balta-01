using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get.GetCityById
{
    public sealed class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, Result<CityViewModel>>
    {
        private readonly ICityRepository _cityRepository;
        public GetCityByIdHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<Result<CityViewModel>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var citie = await _cityRepository.GetByIdAsync(request.id);

            CityViewModel cityViewModel = new CityViewModel
            {
                Id = citie!.Id,
                Name = citie!.Name,
                State = citie!.State
            };

            return Result.Success(cityViewModel);
        }
    }
}
