using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Orders;

public static class OrderRegistration
{
    public static IHostApplicationBuilder UseOrderProjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<OrderProjection>();
        
        return builder;
    }
}