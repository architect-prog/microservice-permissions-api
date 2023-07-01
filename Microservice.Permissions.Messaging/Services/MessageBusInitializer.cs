using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microservice.Permissions.Messaging.Services.Interfaces;
using Microservice.Permissions.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Consumer = RabbitMQ.Client.Events.AsyncEventingBasicConsumer;
using Handler = RabbitMQ.Client.Events.AsyncEventHandler<RabbitMQ.Client.Events.BasicDeliverEventArgs>;

namespace Microservice.Permissions.Messaging.Services;

public sealed class MessageBusInitializer : IMessageBusInitializer
{
    private readonly BusSettings busSettings;
    private readonly IServiceScope serviceScope;
    private readonly IChannelProvider channelProvider;
    private readonly ILogger<MessageBusInitializer> logger;

    private readonly Dictionary<Consumer, Handler> handlers = new();

    public MessageBusInitializer(
        BusSettings busSettings,
        IServiceScope serviceScope,
        IChannelProvider channelProvider,
        ILogger<MessageBusInitializer> logger)
    {
        this.busSettings = busSettings;
        this.serviceScope = serviceScope;
        this.channelProvider = channelProvider;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var channel = channelProvider.Channel;
        var exchangeSettings = busSettings.Exchanges;
        var services = serviceScope.ServiceProvider;

        foreach (var exchange in exchangeSettings)
        {
            channel.ExchangeDeclare(exchange.Name, ExchangeType.Direct);
            logger.LogInformation("Exchange declared. Name: {Name}", exchange.Name);

            foreach (var queue in exchange.Queues)
            {
                channel.QueueDeclare(queue.Name, exclusive: false, autoDelete: false);
                channel.QueueBind(queue.Name, exchange.Name, queue.Name);
                logger.LogInformation("Queue declared. Name: {Name}", queue.Name);

                if (services.GetRequiredService(queue.HandlerType) is not IMessageHandler messageHandler)
                    throw new InvalidOperationException($"Can't resolve handler of type {queue.HandlerType}.");

                var consumer = new AsyncEventingBasicConsumer(channel);
                Handler handler = async (_, args) => await messageHandler.Handle(args);
                consumer.Received += handler;

                handlers.Add(consumer, handler);

                channel.BasicConsume(queue.Name, autoAck: true, consumer: consumer);
                logger.LogInformation("Handler of type: {HandlerType} attached to queue", queue.HandlerType);
            }
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var (consumer, handler) in handlers)
            consumer.Received -= handler;

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        channelProvider.Dispose();
        serviceScope.Dispose();;
    }
}