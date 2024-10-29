using EventStore.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventStore.EventStoreDb;

public static class InfrastructureRegistration
{
    public static IHostApplicationBuilder UseEventStoreDb(this IHostApplicationBuilder builder)
    {
        EventStoreDbOptions dbOptions = new();
            
        builder.Configuration.GetSection(nameof(EventStoreDbOptions)).Bind(dbOptions);
        
        var settings = EventStoreClientSettings.Create(dbOptions.ConnectionString);
        var client = new EventStoreClient(settings);
        
        builder.Services.AddEventStoreClient(dbOptions.ConnectionString);
        builder.Services.AddSingleton<IEventStoreRepository, EventStoreDbRepository>();
        
        return builder;
    }
}