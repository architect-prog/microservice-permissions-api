using ArchitectProg.FunctionalExtensions.Extensions;
using Microservice.Permissions.Azure.Bus.Factories.Interfaces;
using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microservice.Permissions.Azure.Bus.Settings;

namespace Microservice.Permissions.Azure.Bus.Factories;

public sealed class MessageBusSettingsBuilder : IMessageBusSettingsBuilder
{
    private readonly BusSettings busSettings = new();

    public IMessageBusSettingsBuilder RegisterHandler<THandlerType>(string queueName)
        where THandlerType : IMessageHandler
    {
        if (queueName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(queueName));

        var queueSettings = new QueueSettings
        {
            Name = queueName,
            HandlerType = typeof(THandlerType)
        };

        busSettings.AddQueue(queueSettings);
        return this;
    }

    public BusSettings Build()
    {
        return busSettings;
    }
}