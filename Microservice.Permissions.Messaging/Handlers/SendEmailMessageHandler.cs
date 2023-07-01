using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace Microservice.Permissions.Messaging.Handlers;

public sealed class SendEmailMessageHandler : IMessageHandler
{
    private readonly ILogger<SendEmailMessageHandler> logger;

    public SendEmailMessageHandler(ILogger<SendEmailMessageHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(BasicDeliverEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Information, "email sent");
        return Task.CompletedTask;
    }
}