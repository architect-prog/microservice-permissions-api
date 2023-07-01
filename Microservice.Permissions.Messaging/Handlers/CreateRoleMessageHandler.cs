﻿using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Messaging.Extensions;
using Microservice.Permissions.Messaging.Handlers.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace Microservice.Permissions.Messaging.Handlers;

public sealed class CreateRoleMessageHandler : IMessageHandler
{
    private readonly IRoleService roleService;
    private readonly ILogger<CreateRoleMessageHandler> logger;

    public CreateRoleMessageHandler(
        IRoleService roleService,
        ILogger<CreateRoleMessageHandler> logger)
    {
        this.roleService = roleService;
        this.logger = logger;
    }

    public async Task Handle(BasicDeliverEventArgs args)
    {
        if (args == null)
            throw new ArgumentNullException(nameof(args));

        logger.Log(LogLevel.Information, "CreateRoleMessageHandler handling started");

        var message = args.Body.FromBytes().Deserialize<CreateRoleRequest>();
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
}