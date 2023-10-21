namespace Balta.IBGE.Domain.Core.Entities;

public abstract class Entity
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; init; }
}