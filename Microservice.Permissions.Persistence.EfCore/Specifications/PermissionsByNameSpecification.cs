using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications
{
    public class PermissionsByNameSpecification : Specification<AreaRolePermissionsEntity>
    {
        private readonly string roleName;
        private readonly string areaName;
        private readonly string applicationName;

        public PermissionsByNameSpecification(string roleName, string areaName, string applicationName)
        {
            this.roleName = roleName;
            this.areaName = areaName;
            this.applicationName = applicationName;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddEagerFetching(IQueryable<AreaRolePermissionsEntity> query)
        {
            //var result = query.Include(x => x.Role).Include(x => x.Area).ThenInclude(x => x.Application);

            return query;
        }

        public override IQueryable<AreaRolePermissionsEntity> AddPredicates(IQueryable<AreaRolePermissionsEntity> query)
        {
            var result = query.Where(x => x.Area != null &&
                                          x.Role != null &&
                                          x.Area.Application != null &&
                                          x.Area.Name != null &&
                                          x.Role.Name != null &&
                                          x.Area.Application.Name != null &&
                                          EF.Functions.ILike(x.Area.Name, $"%{areaName}%") &&
                                          EF.Functions.ILike(x.Role.Name, $"%{roleName}%") &&
                                          EF.Functions.ILike(x.Area.Application.Name, $"%{applicationName}%"));

            return result;
        }
    }
}