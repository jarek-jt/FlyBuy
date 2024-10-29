namespace ServiceBus;

public interface IServiceBus
{
    void Produce<T>(T message);
    public T Consume<T>() where T : new();
}