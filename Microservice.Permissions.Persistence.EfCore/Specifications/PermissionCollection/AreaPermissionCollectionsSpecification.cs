using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications.PermissionCollection
{
    public sealed class AreaPermissionCollectionsSpecification : Specification<PermissionCollectionEntity>
    {
        private readonly int areaId;

        public AreaPermissionCollectionsSpecification(int areaId)
        {
            this.areaId = areaId;
        }

        public override IQueryable<PermissionCollectionEntity> AddEagerFetching(
            IQueryable<PermissionCollectionEntity> query)
        {
            var result = query.Include(x => x.Permissions);
            return result;
        }

        public override IQueryable<PermissionCollectionEntity> AddPredicates(
            IQueryable<PermissionCollectionEntity> query)
        {
            var result = query.Where(x => x.AreaId == areaId);
            return result;
        }
    }
}