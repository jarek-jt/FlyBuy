using System;
using FluentAssertions;
using JetBrains.Annotations;
using Orders.Domain;
using Orders.Events;
using Xunit;

namespace Orders.Tests.Domain;

[TestSubject(typeof(Order))]
public class OrderTest
{

    [Fact]
    public void OrderCreatedShouldSetOrderId()
    {
        //Given
        var order = new Order();
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        
        var testEvent = new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = customerId,
            Created = DateTime.Now
        };
        
        //When
        order.Apply(testEvent);
        
        //Then
        order.Id.Should().Be(orderId);
        order.CustomerId.Should().Be(customerId);
    }
    
    [Fact]
    public void OrderCancelledShouldChangeStatus()
    {
        //Given
        var order = new Order();
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        
        var testEvent = new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = customerId,
            Created = DateTime.Now
        };
        
        var testEvent2 = new OrderCancelled()
        {
            OrderId = orderId,
            Created = DateTime.Now
        };
        
        //When
        order.Apply(testEvent);
        order.Apply(testEvent2);
        
        //Then
        order.Status.Should().Be(OrderStatus.Canceled);
    }
    
    [Fact]
    public void OrderProductAddedShouldContainProductId()
    {
        //Given
        var order = new Order();
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        
        var testEvent = new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = customerId,
            Created = DateTime.Now
        };

        var testEvent2 = new ProductAdded()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = 1
        };
        
        //When
        order.Apply(testEvent);
        order.Apply(testEvent2);
        
        //Then
        order.Products.Should().ContainKey(productId);
    }
    
    [Fact]
    public void OrderProductAddedShouldIncreaseQuantityInExistingProduct()
    {
        //Given
        var order = new Order();
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        
        var testEvent = new OrderCreated()
        {
            OrderId = orderId,
            CustomerId = customerId,
            Created = DateTime.Now
        };

        var testEvent2 = new ProductAdded()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = 1
        };
        
        var testEvent3 = new ProductAdded()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = 1
        };
        
        //When
        order.Apply(testEvent);
        order.Apply(testEvent2);
        order.Apply(testEvent3);
        
        //Then
        order.Products.Should().ContainKey(productId);
        order.Products[productId].Quantity.Should().Be(2);
    }
}