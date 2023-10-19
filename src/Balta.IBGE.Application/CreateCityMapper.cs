using AutoMapper;

using Balta.IBGE.Domain.Cities;

namespace Balta.IBGE.Application;

public class CreateCityMapper : Profile
{
    public CreateCityMapper()
    {
        CreateMap<CreateCityRequest, City>();
        CreateMap<City, CreateCityResponse>();
    }
}