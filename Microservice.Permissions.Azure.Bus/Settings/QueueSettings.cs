namespace Microservice.Permissions.Azure.Bus.Settings;

public sealed class QueueSettings
{
    public required string Name { get; init; }
    public required Type HandlerType { get; init; }
}