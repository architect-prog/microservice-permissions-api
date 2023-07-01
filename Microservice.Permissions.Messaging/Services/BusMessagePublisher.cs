using Microservice.Permissions.Messaging.Contracts;
using Microservice.Permissions.Messaging.Extensions;
using Microservice.Permissions.Messaging.Services.Interfaces;
using RabbitMQ.Client;

namespace Microservice.Permissions.Messaging.Services;

public class BusMessagePublisher : IBusMessagePublisher
{
    private readonly IChannelProvider channelProvider;

    public BusMessagePublisher(IChannelProvider channelProvider)
    {
        this.channelProvider = channelProvider;
    }

    public Task PublishMessage<T>(BusMessage<T> message)
    {
        var channel = channelProvider.Channel;
        var body = message.Payload.Serialize().ToBytes();
        channel.BasicPublish(message.Exchange, message.Queue, body: body);

        return Task.CompletedTask;
    }
}