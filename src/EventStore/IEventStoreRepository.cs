namespace EventStore;

public interface IEventStoreRepository
{
    public Task<long> AppendAsync<T>(byte[] @event, byte[] metadata, string streamId);
    public Task<List<T>> GetAsync<T>(Guid id);
}