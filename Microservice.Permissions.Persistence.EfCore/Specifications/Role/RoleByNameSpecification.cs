using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Persistence.EfCore.Specifications.Role;

public sealed class RoleByNameSpecification : Specification<RoleEntity>
{
    private readonly string name;

    public RoleByNameSpecification(string name)
    {
        this.name = name;
    }

    public override IQueryable<RoleEntity> AddPredicates(IQueryable<RoleEntity> query)
    {
        var result = query.Where(x => x.Name.ToUpper() == name.ToUpper());
        return result;
    }
}