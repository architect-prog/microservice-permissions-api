using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications
{
    public class PermissionsSpecification : Specification<AreaRolePermissionsEntity>
    {
        private readonly int[]? roleIds;
        private readonly int[]? areaIds;

        public PermissionsSpecification(int[]? roleIds, int[]? areaIds)
        {
            this.roleIds = roleIds;
            this.areaIds = areaIds;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddPredicates(IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.AsQueryable();
            if (roleIds is not null && roleIds.Any())
                result = result.Where(x => roleIds.Any(y => x.RoleId == y));

            if (areaIds is not null && areaIds.Any())
                result = query.Where(x => areaIds.Any(y => x.AreaId == y));

            return result;
        }
    }
}