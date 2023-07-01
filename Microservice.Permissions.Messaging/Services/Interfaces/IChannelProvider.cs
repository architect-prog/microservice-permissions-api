using RabbitMQ.Client;

namespace Microservice.Permissions.Messaging.Services.Interfaces;

public interface IChannelProvider : IDisposable
{
    IModel Channel { get; }
    IConnection Connection { get; }
}