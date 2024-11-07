using MyEventStore.EventStoreDb;
using Orders;
using Orders.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder
    .UseEventStoreDb()
    .UseOrderProjection();


var host = builder.Build();

host.Run();