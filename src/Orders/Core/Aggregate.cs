using Orders.Events;

namespace Orders.Core;

public abstract class Aggregate
{
    public Guid Id { get; protected set; } = default!;

    public abstract void Apply(Event orderEvent)
    {
    }
}