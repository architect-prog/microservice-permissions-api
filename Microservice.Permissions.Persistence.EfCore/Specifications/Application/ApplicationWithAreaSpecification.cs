using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Specifications.Application;

public class ApplicationWithAreaSpecification : Specification<ApplicationEntity>
{
    private readonly int applicationId;
    private readonly string areaName;

    public ApplicationWithAreaSpecification(int applicationId, string areaName)
    {
        this.applicationId = applicationId;
        this.areaName = areaName;
    }

    public override IQueryable<ApplicationEntity> AddEagerFetching(IQueryable<ApplicationEntity> query)
    {
        var result = query.Include(x => x.Areas.Where(y => y.Name != null && y.Name.ToUpper() == areaName.ToUpper()));
        return result;
    }

    public override IQueryable<ApplicationEntity> AddPredicates(IQueryable<ApplicationEntity> query)
    {
        var result = query.Where(x => x.Id == applicationId);
        return result;
    }
}