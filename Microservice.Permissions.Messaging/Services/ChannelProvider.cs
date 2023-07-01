using Microservice.Permissions.Messaging.Services.Interfaces;
using Microservice.Permissions.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Microservice.Permissions.Messaging.Services;

public sealed class ChannelProvider : IChannelProvider
{
    private readonly MessagingSettings messagingSettings;

    private readonly Lazy<IModel> channel;
    private readonly Lazy<IConnection> connection;

    public IModel Channel => channel.Value;
    public IConnection Connection => connection.Value;

    public ChannelProvider(IOptions<MessagingSettings> messagingSettings)
    {
        this.messagingSettings = messagingSettings.Value;

        connection = new Lazy<IConnection>(GetConnection, LazyThreadSafetyMode.ExecutionAndPublication);
        channel = new Lazy<IModel>(GetChannel, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    private IConnection GetConnection()
    {
        var uri = new Uri(messagingSettings.ConnectionString);
        var connectionFactory = new ConnectionFactory
        {
            Uri = uri,
            DispatchConsumersAsync = true
        };

        var result = connectionFactory.CreateConnection();
        return result;
    }

    private IModel GetChannel()
    {
        var result = Connection.CreateModel();
        return result;
    }

    public void Dispose()
    {
        if (connection.IsValueCreated)
            Connection.Dispose();
        if (channel.IsValueCreated)
            Channel.Dispose();
    }
}