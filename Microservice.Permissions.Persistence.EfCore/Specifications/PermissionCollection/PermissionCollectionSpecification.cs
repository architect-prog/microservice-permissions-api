using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications.PermissionCollection;

public sealed class PermissionCollectionSpecification : Specification<PermissionCollectionEntity>
{
    private readonly string application;
    private readonly string area;
    private readonly string role;

    public PermissionCollectionSpecification(string application, string area, string role)
    {
        this.application = application;
        this.area = area;
        this.role = role;
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
        var result = query.Where(x => x.Role.Name.ToUpper() == role.ToUpper()
                                      && x.Area.Name.ToUpper() == area.ToUpper()
                                      && x.Area.Application.Name.ToUpper() == application.ToUpper());
        return result;
    }
}