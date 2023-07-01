namespace Microservice.Permissions.Messaging.Contracts;

public class BusMessage<T>
{
    public required string Queue { get; init; }
    public required string Exchange { get; init; }
    public required T Payload { get; init; }
}