using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications;

public class RolesSpecification : Specification<RoleEntity>
{
    private readonly int[] roleIds;

    public RolesSpecification(params int[] roleIds)
    {
        this.roleIds = roleIds;
    }

    public override IQueryable<RoleEntity> AddPredicates(IQueryable<RoleEntity> query)
    {
        var result = query.Where(x => roleIds.Any(y => x.Id == y));
        return result;
    }
}