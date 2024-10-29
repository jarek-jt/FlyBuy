using Microsoft.AspNetCore.Mvc;
using Order.Api.Requests;
using Orders;
using Orders.Events;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly OrderRepository _repository;

    public OrdersController(ILogger<OrdersController> logger, OrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var createOrder = OrderCreated.Create(request.OrderId, request.CustomerId);
        var result = await _repository.AppendAsync<Orders.Domain.Order, OrderCreated>(createOrder);
        
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> OrderCancelled(Guid orderId)
    { 
        var orderCancelled = Orders.Events.OrderCancelled.Create(orderId);
        var result = await _repository.AppendAsync<Orders.Domain.Order, OrderCancelled>(orderCancelled);
        
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> OrderFinalized(Guid orderId)
    { 
        var orderFinalized = Orders.Events.OrderFinalized.Create(orderId);
        var result = await _repository.AppendAsync<Orders.Domain.Order, OrderFinalized>(orderFinalized);
        
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> ProductAdded(Guid orderId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<IActionResult> ProductRemoverd(Guid orderId)
    { 
        throw new NotImplementedException();
    }
}