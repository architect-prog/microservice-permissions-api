using Azure.Messaging.ServiceBus;

namespace Microservice.Permissions.Azure.Bus.Handlers.Interfaces;

public interface IMessageHandler
{
    Task Handle(ProcessMessageEventArgs args);
    Task HandleError(ProcessErrorEventArgs args);
}