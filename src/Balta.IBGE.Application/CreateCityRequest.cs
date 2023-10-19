using MediatR;

namespace Balta.IBGE.Application;

public sealed record CreateCityRequest(int Id, string Name, string State) : IRequest<CreateCityRequest>;