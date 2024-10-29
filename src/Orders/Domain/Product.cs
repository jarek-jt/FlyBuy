using Orders.Core;

namespace Orders.Domain;

public record Product(Guid Id, string Name) : ValueObject;