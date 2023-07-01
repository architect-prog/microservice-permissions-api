using Azure.Messaging.ServiceBus;
using Microservice.Permissions.Azure.Bus.Extensions;
using Microservice.Permissions.Azure.Bus.Handlers.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microservice.Permissions.Azure.Bus.Handlers;

public sealed class CreateRoleAzureMessageHandler : IMessageHandler
{
    private readonly IRoleService roleService;
    private readonly ILogger<CreateRoleAzureMessageHandler> logger;

    public CreateRoleAzureMessageHandler(
        IRoleService roleService,
        ILogger<CreateRoleAzureMessageHandler> logger)
    {
        this.roleService = roleService;
        this.logger = logger;
    }

    public async Task Handle(ProcessMessageEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Information, "CreateRoleMessageHandler handling started");

        var body = args.Message.Body.ToString();
        var message = body.Deserialize<CreateRoleRequest>();

        if (message is null)
            throw new InvalidCastException("Message body must be non-empty and have consistent type");

        var result = await roleService.Create(message);

        if (!result.IsSuccess)
        {
            logger.Log(LogLevel.Information,
                "CreateRoleMessageHandler handling finished. Status: failure. Error: {Message}",
                result.Exception?.Message);

            return;
        }

        logger.Log(LogLevel.Information, "CreateRoleMessageHandler handling finished. Status: success");
    }

    public Task HandleError(ProcessErrorEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Error, args.Exception?.Message);
        return Task.CompletedTask;
    }
}