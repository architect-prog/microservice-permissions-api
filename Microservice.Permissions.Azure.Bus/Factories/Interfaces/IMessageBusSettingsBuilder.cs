using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microservice.Permissions.Azure.Bus.Settings;

namespace Microservice.Permissions.Azure.Bus.Factories.Interfaces;

public interface IMessageBusSettingsBuilder
{
    IMessageBusSettingsBuilder RegisterHandler<THandlerType>(string queueName) where THandlerType : IMessageHandler;
    BusSettings Build();
}