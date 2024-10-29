using Orders.Events;

namespace Orders.Core;

public abstract class Aggregate
{
    public Guid Id { get; protected set; } = default!;

    public virtual void Apply(Event orderEvent)
    {
    }
}