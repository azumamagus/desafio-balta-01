using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Delete;

public record DeleteCityCommand(
    int Id) : IRequest<Result>;