namespace Order.Api.Requests;

public record CreateOrderRequest(
    Guid OrderId,
    Guid CustomerId
    );