using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators
{
    public sealed class AreaRoleCreator : IAreaRoleCreator
    {
        public PermissionCollectionEntity Create(int roleId, int areaId)
        {
            var result = new PermissionCollectionEntity
            {
                RoleId = roleId,
                AreaId = areaId
            };

            return result;
        }

        public IEnumerable<PermissionCollectionEntity> CreateForRole(int roleId, IEnumerable<int> areaIds)
        {
            var result = areaIds.Select(x => Create(roleId, x));
            return result;
        }

        public IEnumerable<PermissionCollectionEntity> CreateForArea(int areaId, IEnumerable<int> roleIds)
        {
            var result = roleIds.Select(x => Create(x, areaId));
            return result;
        }
    }
}