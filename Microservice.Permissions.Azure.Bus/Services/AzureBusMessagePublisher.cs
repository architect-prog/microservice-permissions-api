using Azure.Messaging.ServiceBus;
using Microservice.Permissions.Azure.Bus.Contracts;
using Microservice.Permissions.Azure.Bus.Extensions;
using Microservice.Permissions.Azure.Bus.Services.Interfaces;

namespace Microservice.Permissions.Azure.Bus.Services;

public class AzureBusMessagePublisher : IAzureBusMessagePublisher
{
    private readonly IServiceBusClientProvider serviceBusClientProvider;

    public AzureBusMessagePublisher(IServiceBusClientProvider serviceBusClientProvider)
    {
        this.serviceBusClientProvider = serviceBusClientProvider;
    }

    public async Task PublishMessage<T>(BusMessage<T> message)
    {
        var client = serviceBusClientProvider.Client;
        await using (var sender = client.CreateSender(message.Queue))
        {
            var body = message.Payload.Serialize().ToBytes();
            var busMessage = new ServiceBusMessage(body);
            await sender.SendMessageAsync(busMessage);
        }
    }
}