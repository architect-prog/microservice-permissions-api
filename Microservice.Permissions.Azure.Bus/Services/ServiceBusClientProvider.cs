using Azure.Messaging.ServiceBus;
using Microservice.Permissions.Azure.Bus.Services.Interfaces;
using Microservice.Permissions.Azure.Bus.Settings;
using Microsoft.Extensions.Options;

namespace Microservice.Permissions.Azure.Bus.Services;

public sealed class ServiceBusClientProvider : IServiceBusClientProvider
{
    private readonly ServiceBusSettings serviceBusSettings;

    private readonly Lazy<ServiceBusClient> client;

    public ServiceBusClient Client => client.Value;

    public ServiceBusClientProvider(IOptions<ServiceBusSettings> serviceBusSettings)
    {
        this.serviceBusSettings = serviceBusSettings.Value;

        client = new Lazy<ServiceBusClient>(GetClient, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    private ServiceBusClient GetClient()
    {
        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        var result = new ServiceBusClient(serviceBusSettings.ConnectionString, clientOptions);
        return result;
    }

    public async ValueTask DisposeAsync()
    {
        if (client.IsValueCreated)
            await Client.DisposeAsync();
    }
}