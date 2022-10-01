using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Database.Specifications.Area;

public sealed class ApplicationAreasSpecification : Specification<AreaEntity>
{
    private readonly int applicationId;

    public ApplicationAreasSpecification(int applicationId)
    {
        this.applicationId = applicationId;
    }

    public override IQueryable<AreaEntity> AddPredicates(IQueryable<AreaEntity> query)
    {
        var result = query.Where(x => x.ApplicationId == applicationId);
        return result;
    }
}