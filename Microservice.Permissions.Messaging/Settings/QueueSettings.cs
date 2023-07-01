namespace Microservice.Permissions.Messaging.Settings;

public sealed class QueueSettings
{
    public required string Name { get; init; }
    public required Type HandlerType { get; init; }
}