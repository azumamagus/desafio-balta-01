namespace Balta.IBGE.Application;

public sealed record CreateCityResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
}