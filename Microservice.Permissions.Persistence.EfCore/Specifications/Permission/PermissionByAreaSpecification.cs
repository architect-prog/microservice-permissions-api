using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications.Permission
{
    public sealed class PermissionByAreaSpecification : Specification<PermissionEntity>
    {
        private readonly int areaId;
        private readonly string[] permissions;

        public PermissionByAreaSpecification(int areaId, params string[] permissions)
        {
            this.areaId = areaId;
            this.permissions = permissions;
        }

        public override IQueryable<PermissionEntity> AddEagerFetching(
            IQueryable<PermissionEntity> query)
        {
            var result = query.Include(x => x.PermissionCollection);
            return result;
        }

        public override IQueryable<PermissionEntity> AddPredicates(
            IQueryable<PermissionEntity> query)
        {
            var result = query.Where(x => x.PermissionCollection.AreaId == areaId
                                          && permissions.Any(y => x.Name.ToUpper() == y.ToUpper()));
            return result;
        }
    }
}