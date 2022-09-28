using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces
{
    public interface IAreaRoleCreator
    {
        AreaRolePermissionsEntity Create(int roleId, int areaId);
        IEnumerable<AreaRolePermissionsEntity> CreateForRole(int roleId, IEnumerable<int> areaIds);
        IEnumerable<AreaRolePermissionsEntity> CreateForArea(int areaId, IEnumerable<int> roleIds);
    }
}