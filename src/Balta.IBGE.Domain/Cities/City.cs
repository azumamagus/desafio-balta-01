namespace Balta.IBGE.Domain.Cities;

public class City
{
    public City(
        int id,
        string state,
        string name)
    {
        Id = id;
        State = state;
        Name = name;
    }

    protected City()
    { }

    public int Id { get; private set; }
    public string State { get; private set; } = null!;
    public string Name { get; private set; } = null!;
}
