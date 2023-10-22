using Balta.IBGE.Application.UseCases.Accounts.Register;

using Carter;

using MediatR;

namespace Balta.IBGE.Api.UsesCases.Accounts;

public class AccountsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var accountsGroup = app
            .MapGroup("accounts")
            .WithTags("Accounts");


        accountsGroup.MapPost("register", async (RegisterUserCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.UnprocessableEntity(result.Errors.ToList())
                : Results.Created($"/accounts/{result.Value}", result.Value);
        });

        accountsGroup.MapPost("login", ()
            => Results.Ok("Efetuar o login do usuário"));
    }
}
