namespace Balta.IBGE.Domain.Cities;

public class City
{
    protected City()
    { }

    public int Id { get; private set; }
    public string State { get; private set; } = null!;
    public string Name { get; private set; } = null!;
}