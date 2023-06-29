using ArchitectProg.FunctionalExtensions.Extensions;
using ArchitectProg.Kernel.Extensions.Mappers;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class PermissionMapper : Mapper<PermissionEntity, PermissionResponse>, IPermissionMapper
{
    public override PermissionResponse Map(PermissionEntity source)
    {
        var isDefault = PermissionConstants.Defaults.Any(x => source.Name.EqualsIgnoreCase(x));
        var result = new PermissionResponse(source.Name, source.HaveAccess, isDefault);

        return result;
    }
}