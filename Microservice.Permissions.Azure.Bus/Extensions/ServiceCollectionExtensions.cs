using Microservice.Permissions.Azure.Bus.Factories;
using Microservice.Permissions.Azure.Bus.Factories.Interfaces;
using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microservice.Permissions.Azure.Bus.Services;
using Microservice.Permissions.Azure.Bus.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microservice.Permissions.Azure.Bus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureMessageBus(
        this IServiceCollection serviceCollection,
        Action<IMessageBusSettingsBuilder> configure)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));
        if (configure is null)
            throw new ArgumentNullException(nameof(configure));

        serviceCollection.AddScoped<IServiceBusClientProvider, ServiceBusClientProvider>();
        serviceCollection.AddScoped<IMessageBusSettingsBuilder, MessageBusSettingsBuilder>();
        serviceCollection.AddScoped<IAzureBusMessagePublisher, AzureBusMessagePublisher>();

        serviceCollection.AddHostedService<MessageBusInitializer>(scopeProvider =>
        {
            var serviceScope = scopeProvider.CreateScope();
            var channel = serviceScope.ServiceProvider.GetRequiredService<IServiceBusClientProvider>();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<MessageBusInitializer>>();

            var builder = serviceScope.ServiceProvider.GetRequiredService<IMessageBusSettingsBuilder>();
            configure.Invoke(builder);

            var settings = builder.Build();

            var result = new MessageBusInitializer(settings, serviceScope, channel, logger);
            return result;
        });

        return serviceCollection;
    }

    public static IServiceCollection AddAzureMessageHandler<TMessageHandler>(this IServiceCollection serviceCollection)
        where TMessageHandler : class, IMessageHandler
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<TMessageHandler>();

        return serviceCollection;
    }
}