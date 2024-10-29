using System.Text.Json.Serialization;

namespace Orders.Events;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$Type")]
[JsonDerivedType(typeof(OrderCancelled), nameof(OrderCancelled))]
[JsonDerivedType(typeof(OrderCreated), nameof(OrderCreated))]
[JsonDerivedType(typeof(OrderFinalized), nameof(OrderFinalized))]
[JsonDerivedType(typeof(ProductAdded), nameof(ProductAdded))]
[JsonDerivedType(typeof(ProductRemoved), nameof(ProductRemoved))]
public abstract class Event
{
    public abstract Guid StreamId { get; }
    public DateTime Created { get; set; }
}