using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microservice.Permissions.Messaging.Settings;

namespace Microservice.Permissions.Messaging.Factories.Interfaces;

public interface IMessageBusSettingsBuilder
{
    IMessageBusSettingsBuilder RegisterExchange(string name);
    IMessageBusSettingsBuilder RegisterHandler<THandlerType>(string queueName) where THandlerType : IMessageHandler;
    BusSettings Build();
}