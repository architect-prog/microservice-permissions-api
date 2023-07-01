namespace Microservice.Permissions.Azure.Bus.Contracts;

public class BusMessage<T>
{
    public required string Queue { get; init; }
    public required T Payload { get; init; }
}