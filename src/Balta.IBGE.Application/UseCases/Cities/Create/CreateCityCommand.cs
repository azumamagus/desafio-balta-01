using Balta.IBGE.Domain.Core;

using FluentValidation;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Create;

public record CreateCityCommand(
    int Id,
    string State,
    string Name) : IRequest<Result<int>>;

public sealed class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id can't be empty");

        RuleFor(x => x.State)
             .NotEmpty().WithMessage("State can't be empty")
             .Length(2, 2).WithMessage("State should be 2 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can't be empty")
            .MaximumLength(100).WithMessage("Name should be max 100 characters");
    }
}