using Balta.IBGE.Application.UseCases.Cities.Create;
using Balta.IBGE.Application.UseCases.Cities.Delete;
using Balta.IBGE.Application.UseCases.Cities.GetById;
using Balta.IBGE.Application.UseCases.Cities.List;
using Balta.IBGE.Application.UseCases.Cities.Put;

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

        citiesGroup.MapGet(string.Empty, async ([AsParameters] ListCityOptions filterOptions, ISender sender) =>
        {
            var result = await sender.Send(new ListCityQuery(filterOptions));
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Ok(result.Value);
        });

        citiesGroup.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetCityByIdQuery(id));

            return result.IsFailure
                ? Results.NotFound(result.Errors.ToList())
                : Results.Ok(result.Value);
        });

        citiesGroup.MapPost(string.Empty, async (CreateCityCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Created($"/cities/{result.Value}", result.Value);
        });

        citiesGroup.MapPut("{id:int}", async (int id, PutCityCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            return result.IsFailure 
            ? Results.UnprocessableEntity(result.Errors.ToList()) 
            : Results.NoContent();
        });


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