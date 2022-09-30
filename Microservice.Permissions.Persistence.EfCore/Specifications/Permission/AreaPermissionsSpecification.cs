using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications.Permission
{
    public sealed class AreaPermissionsSpecification : Specification<AreaRolePermissionsEntity>
    {
        private readonly int areaId;

        public AreaPermissionsSpecification(int areaId)
        {
            this.areaId = areaId;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddEagerFetching(
            IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.Include(x => x.Permissions);
            return result;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddPredicates(
            IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.Where(x => x.AreaId == areaId);
            return result;
        }
    }
}