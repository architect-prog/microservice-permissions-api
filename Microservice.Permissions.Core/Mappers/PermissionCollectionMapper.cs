using ArchitectProg.FunctionalExtensions.Extensions;
using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Factories.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class PermissionCollectionMapper :
    Mapper<PermissionCollectionEntity, PermissionCollectionResponse>,
    IPermissionCollectionMapper
{
    private readonly IPermissionMapper permissionMapper;
    private readonly IDefaultPermissionFactory defaultPermissionFactory;

    public PermissionCollectionMapper(
        IPermissionMapper permissionMapper,
        IDefaultPermissionFactory defaultPermissionFactory)
    {
        this.permissionMapper = permissionMapper;
        this.defaultPermissionFactory = defaultPermissionFactory;
    }

    public override PermissionCollectionResponse Map(PermissionCollectionEntity source)
    {
        var permissions = permissionMapper
            .MapCollection(source.Permissions)
            .ToArray();

        var defaultPermissions = defaultPermissionFactory.CreateDefaultPermissions();
        var nonExistingDefaultPermissions = defaultPermissions
            .ExceptBy(permissions.Select(x => x.Name), x => x.Name);

        var result = new PermissionCollectionResponse(
            source.RoleId,
            source.AreaId,
            permissions.Concat(nonExistingDefaultPermissions)
        );

        return result;
    }
}