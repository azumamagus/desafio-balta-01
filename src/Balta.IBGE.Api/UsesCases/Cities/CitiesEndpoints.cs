using Balta.IBGE.Application.UseCases.Cities.Create;
using Balta.IBGE.Application.UseCases.Cities.Get;

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

        citiesGroup.MapGet("", async ([AsParameters] ListCityOptions filterOptions, ISender sender) =>
        {
            var result = await sender.Send(new ListCityQuery(filterOptions));
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Ok(result.Value);
        });

        citiesGroup.MapGet("{id:int}", ()
            => Results.Ok("Retornar os dados da cidade filtrada pelo ID."));

        citiesGroup.MapPost("", async (CreateCityCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Created($"/cities/{result.Value}", result.Value);
        });

        citiesGroup.MapPut("{id:int}", ()
            => Results.NoContent());

        citiesGroup.MapDelete("{id:int}", ()
            => Results.NoContent());

        citiesGroup.MapPost("import", ()
            => Results.Ok("Importar a planilha com os dados das cidades"));
    }
}