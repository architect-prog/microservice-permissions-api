using Microservice.Permissions.Messaging.Factories;
using Microservice.Permissions.Messaging.Factories.Interfaces;
using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microservice.Permissions.Messaging.Services;
using Microservice.Permissions.Messaging.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microservice.Permissions.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBus(
        this IServiceCollection serviceCollection,
        Action<IMessageBusSettingsBuilder> configure)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));
        if (configure is null)
            throw new ArgumentNullException(nameof(configure));

        serviceCollection.AddScoped<IChannelProvider, ChannelProvider>();
        serviceCollection.AddScoped<IMessageBusSettingsBuilder, MessageBusSettingsBuilder>();
        serviceCollection.AddScoped<IBusMessagePublisher, BusMessagePublisher>();

        serviceCollection.AddHostedService<MessageBusInitializer>(scopeProvider =>
        {
            var serviceScope = scopeProvider.CreateScope();
            var channel = serviceScope.ServiceProvider.GetRequiredService<IChannelProvider>();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<MessageBusInitializer>>();

            var builder = serviceScope.ServiceProvider.GetRequiredService<IMessageBusSettingsBuilder>();
            configure.Invoke(builder);

            var settings = builder.Build();

            var result = new MessageBusInitializer(settings, serviceScope, channel, logger);
            return result;
        });

        return serviceCollection;
    }

    public static IServiceCollection AddMessageHandler<TMessageHandler>(this IServiceCollection serviceCollection)
        where TMessageHandler : class, IMessageHandler
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<TMessageHandler>();

        return serviceCollection;
    }
}