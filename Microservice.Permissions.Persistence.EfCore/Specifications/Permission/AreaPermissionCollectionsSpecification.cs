using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Persistence.EfCore.Specifications.Permission;

public sealed class AreaPermissionCollectionsSpecification : Specification<PermissionCollectionEntity>
{
    private readonly int areaId;
    private readonly string[] permissions;

    public AreaPermissionCollectionsSpecification(int areaId, params string[] permissions)
    {
        this.areaId = areaId;
        this.permissions = permissions.Select(x => x.ToUpper()).ToArray();
    }

    public override IQueryable<PermissionCollectionEntity> AddEagerFetching(
        IQueryable<PermissionCollectionEntity> query)
    {
        if (!permissions.Any())
        {
            var result = query.Include(x => x.Permissions);
            return result;
        }

        return query.Include(x => x.Permissions.Where(y => permissions.Any(z => y.Name.ToUpper() == z)));
    }

    public override IQueryable<PermissionCollectionEntity> AddPredicates(
        IQueryable<PermissionCollectionEntity> query)
    {
        var result = query.Where(x => x.AreaId == areaId);
        return result;
    }
}