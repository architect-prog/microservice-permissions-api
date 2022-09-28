using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications
{
    public class AreaPermissionsSpecification : Specification<AreaRolePermissionsEntity>
    {
        private readonly int areaId;

        public AreaPermissionsSpecification(int areaId)
        {
            this.areaId = areaId;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddPredicates(IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.Where(x => x.AreaId == areaId);
            return result;
        }
    }
}