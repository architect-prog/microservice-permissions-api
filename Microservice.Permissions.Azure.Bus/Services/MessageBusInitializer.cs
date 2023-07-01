using Azure.Messaging.ServiceBus;
using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microservice.Permissions.Azure.Bus.Services.Interfaces;
using Microservice.Permissions.Azure.Bus.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microservice.Permissions.Azure.Bus.Services;

public sealed class MessageBusInitializer : IMessageBusInitializer
{
    private readonly BusSettings busSettings;
    private readonly IServiceScope serviceScope;
    private readonly IServiceBusClientProvider serviceBusClientProvider;
    private readonly ILogger<MessageBusInitializer> logger;

    private readonly List<ServiceBusProcessor> processors = new();

    public MessageBusInitializer(
        BusSettings busSettings,
        IServiceScope serviceScope,
        IServiceBusClientProvider serviceBusClientProvider,
        ILogger<MessageBusInitializer> logger)
    {
        this.busSettings = busSettings;
        this.serviceScope = serviceScope;
        this.serviceBusClientProvider = serviceBusClientProvider;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = serviceBusClientProvider.Client;
        var services = serviceScope.ServiceProvider;

        foreach (var queue in busSettings.Queues)
        {
            var processor = client.CreateProcessor(queue.Name, new ServiceBusProcessorOptions());

            if (services.GetRequiredService(queue.HandlerType) is not IMessageHandler messageHandler)
                throw new InvalidOperationException($"Can't resolve handler of type {queue.HandlerType}.");

            processor.ProcessMessageAsync += messageHandler.Handle;
            processor.ProcessErrorAsync += messageHandler.HandleError;

            await processor.StartProcessingAsync(cancellationToken);
            processors.Add(processor);

            logger.LogInformation("Handler of type: {HandlerType} attached to queue", queue.HandlerType);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var processor in processors)
            await processor.StopProcessingAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        //serviceScope.Dispose();
        await serviceBusClientProvider.DisposeAsync();
        foreach (var processor in processors)
            await processor.DisposeAsync();
    }
}