using System.Text.Json;

namespace ServiceBus;

public class CustomMqRequestPrice : IServiceBus
{
    private string equeuedMessage;
    
    public void Produce<T>(T message)
    {
        var json = JsonSerializer.Serialize(message);
        //queue.Enqueue(json);
    }

    public T Consume<T>() where T : new()
    {
            return default;   
    }
}