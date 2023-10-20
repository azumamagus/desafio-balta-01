using System.ComponentModel.DataAnnotations;

using Balta.IBGE.Domain.Core;

using FluentValidation;

using MediatR;

namespace Balta.IBGE.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => validationFailure.ErrorMessage)
            .ToList();

        if (validationErrors.Any())
        {
            var responseType = typeof(TResponse);
            var responseGenericType = responseType.GetGenericArguments()[0];

            var unprocessableResponseType = typeof(Result<>).MakeGenericType(responseGenericType);
            
            var unprocessableResponse = Activator.CreateInstance(
                unprocessableResponseType,
                new object?[] {
                    null,
                    validationErrors!.ToList()
                });
            if (unprocessableResponse is TResponse result)
                return await Task.FromResult(result);
        }

        return await next();
    }
}