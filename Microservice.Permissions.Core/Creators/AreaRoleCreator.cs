using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators;

public class AreaRoleCreator : IAreaRoleCreator
{
    public AreaRolePermissionsEntity Create(int roleId, int areaId)
    {
        var result = new AreaRolePermissionsEntity
        {
            RoleId = roleId,
            AreaId = areaId
        };

        return result;
    }

    public IEnumerable<AreaRolePermissionsEntity> CreateForRole(int roleId, IEnumerable<int> areaIds)
    {
        var result = areaIds.Select(x => Create(roleId, x));
        return result;
    }

    public IEnumerable<AreaRolePermissionsEntity> CreateForArea(int areaId, IEnumerable<int> roleIds)
    {
        var result = roleIds.Select(x => Create(x, areaId));
        return result;
    }
}