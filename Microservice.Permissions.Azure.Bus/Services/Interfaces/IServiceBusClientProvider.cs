using Azure.Messaging.ServiceBus;

namespace Microservice.Permissions.Azure.Bus.Services.Interfaces;

public interface IServiceBusClientProvider : IAsyncDisposable
{
    ServiceBusClient Client { get; }
}