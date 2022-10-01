using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces;

public interface IAreaRoleCreator
{
    PermissionCollectionEntity Create(int roleId, int areaId);
    IEnumerable<PermissionCollectionEntity> CreateForRole(int roleId, IEnumerable<int> areaIds);
    IEnumerable<PermissionCollectionEntity> CreateForArea(int areaId, IEnumerable<int> roleIds);
}