﻿using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class ApplicationMapper : Mapper<ApplicationEntity, ApplicationResponse>, IApplicationMapper
{
    public override ApplicationResponse Map(ApplicationEntity source)
    {
        var result = new ApplicationResponse(source.Id, source.Name, source.Description);
        return result;
    }
}