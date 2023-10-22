namespace Balta.IBGE.Application.UseCases.Cities.List;

public record ListCityOptions(
    int? Id,
    string? State,
    string? Name,
    int Page = 1,
    int PageSize = 50)
{
    public bool HasId() => Id is not null;
    public bool HasState() => State is not null;
    public bool HasName() => Name is not null;
}
