using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get.GetAllCities
{
    public sealed class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<IEnumerable<CityViewModel>>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Result<IEnumerable<CityViewModel>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllAsync();
            var cityViewModels = new List<CityViewModel>();

            foreach (var city in cities)
            {
                var cityViewModel = new CityViewModel
                {
                    Id = city.Id,
                    Name = city.Name,
                    State = city.State
                };

                cityViewModels.Add(cityViewModel);
            }

            return Result.Success(cityViewModels.AsEnumerable());
        }
    }

}
