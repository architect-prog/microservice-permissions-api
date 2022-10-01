using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class PermissionMapper : Mapper<PermissionEntity, PermissionResponse>, IPermissionMapper
{
    public override PermissionResponse Map(PermissionEntity source)
    {
        var result = new PermissionResponse
        {
            Name = source.Name,
            HaveAccess = source.HaveAccess
        };

        return result;
    }
}