using FluentValidation;

namespace Balta.IBGE.Application;

public sealed class CreateCityValidator : AbstractValidator<CreateCityResponse>
{
    public CreateCityValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.State).NotEmpty().MaximumLength(2);
    }
}