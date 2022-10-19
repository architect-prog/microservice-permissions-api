using ArchitectProg.FunctionalExtensions.Extensions;
using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class PermissionCollectionDetailsMapper :
    Mapper<PermissionCollectionEntity, PermissionCollectionDetailsResponse>,
    IPermissionCollectionDetailsMapper
{
    private readonly IPermissionMapper permissionMapper;

    public PermissionCollectionDetailsMapper(IPermissionMapper permissionMapper)
    {
        this.permissionMapper = permissionMapper;
    }

    public override PermissionCollectionDetailsResponse Map(PermissionCollectionEntity source)
    {
        var permissions = source.Permissions.ToArray();
        var defaultPermissions = permissions
            .Where(x => PermissionConstants.Defaults.Any(y => x.Name.EqualsIgnoreCase(y)))
            .ToArray();

        var customPermissions = permissions
            .Except(defaultPermissions)
            .ToArray();

        var canReadPermission = defaultPermissions
            .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanRead));
        var canCreatePermission = defaultPermissions
            .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanCreate));
        var canUpdatePermission = defaultPermissions
            .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanUpdate));
        var canDeletePermission = defaultPermissions
            .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanDelete));

        var result = new PermissionCollectionDetailsResponse(
            canCreatePermission?.HaveAccess ?? false,
            canReadPermission?.HaveAccess ?? false,
            canUpdatePermission?.HaveAccess ?? false,
            canDeletePermission?.HaveAccess ?? false,
            permissionMapper.MapCollection(customPermissions));

        return result;
    }
}