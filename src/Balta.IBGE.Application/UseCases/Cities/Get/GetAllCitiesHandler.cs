using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Get
{
    public sealed class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<List<City>>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Result<List<City>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllAsync();

            return Result.Success(cities);
        }
    }

}
