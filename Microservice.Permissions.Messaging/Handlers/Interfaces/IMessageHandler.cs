using RabbitMQ.Client.Events;

namespace Microservice.Permissions.Messaging.Handlers.Interfaces;

public interface IMessageHandler
{
    Task Handle(BasicDeliverEventArgs args);
}