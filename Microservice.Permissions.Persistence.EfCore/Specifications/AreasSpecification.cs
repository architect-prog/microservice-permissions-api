using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications
{
    public class AreasSpecification : Specification<AreaEntity>
    {
        private readonly int[] areaIds;

        public AreasSpecification(params int[] areaIds)
        {
            this.areaIds = areaIds;
        }

        public override IQueryable<AreaEntity> AddPredicates(IQueryable<AreaEntity> query)
        {
            var result = query.Where(x => areaIds.Any(y => x.Id == y));
            return result;
        }
    }
}