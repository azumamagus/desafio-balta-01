using Carter;

namespace Balta.IBGE.Api.UsesCases.Accounts;

public class AccountsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var accountsGroup = app
            .MapGroup("accounts")
            .WithTags("Accounts");


        accountsGroup.MapPost("register", ()
            => Results.Created($"/accounts/login/", 1));

        accountsGroup.MapPost("login", ()
            => Results.Ok("Efetuar o login do usuário"));
    }
}
