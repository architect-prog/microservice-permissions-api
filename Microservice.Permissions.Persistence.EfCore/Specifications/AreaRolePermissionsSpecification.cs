using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications;

public class AreaRolePermissionsSpecification : Specification<AreaRolePermissionsEntity>
{
    private readonly int areaId;
    private readonly int roleId;

    public AreaRolePermissionsSpecification(int areaId, int roleId)
    {
        this.areaId = areaId;
        this.roleId = roleId;
    }

    public override IQueryable<AreaRolePermissionsEntity> AddEagerFetching(IQueryable<AreaRolePermissionsEntity> query)
    {
        var result = query.Include(x => x.Permissions);
        return result;
    }

    public override IQueryable<AreaRolePermissionsEntity> AddPredicates(IQueryable<AreaRolePermissionsEntity> query)
    {
        var result = query.Where(x => x.AreaId == areaId && x.RoleId == roleId);
        return result;
    }
}