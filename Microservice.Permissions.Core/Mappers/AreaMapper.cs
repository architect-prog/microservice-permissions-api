using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public class AreaMapper : Mapper<AreaEntity, AreaResponse>, IAreaMapper
{
    public override AreaResponse Map(AreaEntity source)
    {
        var result = new AreaResponse
        {
            Id = source.Id,
            ApplicationId = source.ApplicationId,
            Name = source.Name
        };

        return result;
    }
}