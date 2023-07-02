﻿using ArchitectProg.FunctionalExtensions.Extensions;
using Microservice.Permissions.Messaging.Factories.Interfaces;
using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microservice.Permissions.Messaging.Settings;

namespace Microservice.Permissions.Messaging.Factories;

public sealed class MessageBusSettingsBuilder : IMessageBusSettingsBuilder
{
    private readonly BusSettings busSettings = new();

    public IMessageBusSettingsBuilder RegisterExchange(string exchangeName)
    {
        if (exchangeName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(exchangeName));

        var exchangeSettings = new ExchangeSettings
        {
            Name = exchangeName,
            Queues = new List<QueueSettings>()
        };

        busSettings.AddExchange(exchangeSettings);
        return this;
    }

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

        var exchange = busSettings.CurrentExchange;
        if (exchange is null)
        {
            var error = "Can't add queue to null exchange. Use MessageBusBuilder.RegisterExchange method first";
            throw new InvalidCastException(error);
        }

        exchange.Queues.Add(queueSettings);
        return this;
    }

    public BusSettings Build()
    {
        return busSettings;
    }
}