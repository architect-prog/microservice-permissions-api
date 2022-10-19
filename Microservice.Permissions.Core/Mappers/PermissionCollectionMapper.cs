using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class PermissionCollectionMapper :
    Mapper<PermissionCollectionEntity, PermissionCollectionResponse>,
    IPermissionCollectionMapper
{
    private readonly IPermissionMapper permissionMapper;

    public PermissionCollectionMapper(IPermissionMapper permissionMapper)
    {
        this.permissionMapper = permissionMapper;
    }

    public override PermissionCollectionResponse Map(PermissionCollectionEntity source)
    {
        var permissions = source.Permissions.ToArray();
        var result = new PermissionCollectionResponse
        {
            RoleId = source.RoleId,
            AreaId = source.AreaId,
            CustomPermissions = permissionMapper.MapCollection(permissions)
        };

        return result;
    }
}