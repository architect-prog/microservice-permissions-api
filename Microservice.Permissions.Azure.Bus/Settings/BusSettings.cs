namespace Microservice.Permissions.Azure.Bus.Settings;

public sealed class BusSettings
{
    private readonly List<QueueSettings> queues = new();
    public IReadOnlyCollection<QueueSettings> Queues => queues;

    public void AddQueue(QueueSettings queue)
    {
        queues.Add(queue);;
    }
}