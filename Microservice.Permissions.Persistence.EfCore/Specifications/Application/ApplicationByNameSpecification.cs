using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications.Application;

public class ApplicationByNameSpecification : Specification<ApplicationEntity>
{
    private readonly string name;

    public ApplicationByNameSpecification(string name)
    {
        this.name = name;
    }

    public override IQueryable<ApplicationEntity> AddPredicates(IQueryable<ApplicationEntity> query)
    {
        var result = query.Where(x => x.Name != null && x.Name.ToUpper() == name.ToUpper());
        return result;
    }
}