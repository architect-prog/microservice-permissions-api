using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Persistence.EfCore.Specifications.Permission;

public sealed class PermissionCollectionsSpecification : Specification<PermissionCollectionEntity>
{
    private readonly int[]? areaIds;
    private readonly int[]? roleIds;

    public PermissionCollectionsSpecification(int[]? areaIds, int[]? roleIds)
    {
        this.areaIds = areaIds;
        this.roleIds = roleIds;
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
        var result = query.AsQueryable();
        if (roleIds is not null && roleIds.Any())
            result = result.Where(x => roleIds.Any(y => x.RoleId == y));

        if (areaIds is not null && areaIds.Any())
            result = result.Where(x => areaIds.Any(y => x.AreaId == y));

        return result;
    }
}