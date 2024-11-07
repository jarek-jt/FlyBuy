namespace MyEventStore.EventStoreDb;

public sealed class EventStoreDbOptions
{
    public string ConnectionString { get; set; }
    public string ConnectionName { get; set; }
    
}