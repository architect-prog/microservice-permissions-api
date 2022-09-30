using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications.Permission
{
    public class PermissionsSpecification : Specification<AreaRolePermissionsEntity>
    {
        private readonly int[]? areaIds;
        private readonly int[]? roleIds;

        public PermissionsSpecification(int[]? areaIds, int[]? roleIds)
        {
            this.areaIds = areaIds;
            this.roleIds = roleIds;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddPredicates(IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.AsQueryable();
            if (roleIds is not null && roleIds.Any())
                result = result.Where(x => roleIds.Any(y => x.RoleId == y));

            if (areaIds is not null && areaIds.Any())
                result = result.Where(x => areaIds.Any(y => x.AreaId == y));

            return result;
        }
    }
}