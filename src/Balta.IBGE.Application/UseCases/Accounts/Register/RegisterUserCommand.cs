using Balta.IBGE.Domain.Core;

using FluentValidation;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Accounts.Register;

public record RegisterUserCommand(
    string Email,
    string Password,
    string PasswordConfirm) : IRequest<Result<Guid>>;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(ruc => ruc.Email)
             .NotEmpty().WithMessage("Email can't be empty")
             .EmailAddress().WithMessage("Invalid email");

        RuleFor(ruc => ruc.Password)
            .NotEmpty()
            .WithMessage("Password can't be empty")
            .Length(8, 25)
            .WithMessage("Password must be between 8 and 25 characters")
            .Equal(ruc => ruc.PasswordConfirm)
            .WithMessage("Password and Password Confirmation aren't the same");
    }
}