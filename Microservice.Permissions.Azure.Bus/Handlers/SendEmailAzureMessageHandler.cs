using Azure.Messaging.ServiceBus;
using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microservice.Permissions.Azure.Bus.Handlers;

public sealed class SendEmailAzureMessageHandler : IMessageHandler
{
    private readonly ILogger<SendEmailAzureMessageHandler> logger;

    public SendEmailAzureMessageHandler(ILogger<SendEmailAzureMessageHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(ProcessMessageEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Information, "email sent");
        return Task.CompletedTask;
    }

    public Task HandleError(ProcessErrorEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Error, args.Exception?.Message);
        return Task.CompletedTask;
    }
}