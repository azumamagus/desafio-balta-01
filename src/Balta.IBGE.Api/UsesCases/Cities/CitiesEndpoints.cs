using Balta.IBGE.Application.UseCases.Cities.Create;
using Balta.IBGE.Application.UseCases.Cities.Delete;
using Balta.IBGE.Application.UseCases.Cities.Get.GetAllCities;
using Balta.IBGE.Application.UseCases.Cities.Get.GetCityById;
using Balta.IBGE.Domain.Core;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Balta.IBGE.Api.UsesCases.Cities;

public class CitiesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var citiesGroup = app
            .MapGroup("cities")
            .WithTags("Cities");

        citiesGroup.MapGet("", async(ISender sender) =>
        {
            var query = new GetAllCitiesQuery();
            var result = await sender.Send(query);

            return result.IsFailure ? Results.NotFound(result.Errors.ToList()) : Results.Ok(result);
        });

        citiesGroup.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            var query = new GetCityByIdQuery(id);
            var result = await sender.Send(query);

            return result.IsFailure ? Results.NotFound(result.Errors.ToList()) : Results.Ok(result);
        });  

        citiesGroup.MapPost("", async (CreateCityCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Created($"/cities/{result.Value}", result.Value);
        });

        citiesGroup.MapPut("{id:int}", ()
            => Results.NoContent());

        citiesGroup.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCityCommand(id));
            return result.IsFailure 
            ? Results.NotFound(result.Errors.ToList()) 
            : Results.NoContent();
        });
            

        citiesGroup.MapPost("import", ()
            => Results.Ok("Importar a planilha com os dados das cidades"));
    }
}