using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Persistence.EfCore.Specifications.Area;

public sealed class AreaByNameSpecification : Specification<AreaEntity>
{
    private readonly int applicationId;
    private readonly string name;

    public AreaByNameSpecification(int applicationId, string name)
    {
        this.applicationId = applicationId;
        this.name = name;
    }

    public override IQueryable<AreaEntity> AddPredicates(IQueryable<AreaEntity> query)
    {
        var result = query
            .Where(x => x.ApplicationId == applicationId)
            .Where(x => x.Name != null && x.Name.ToUpper() == name.ToUpper());

        return result;
    }
}