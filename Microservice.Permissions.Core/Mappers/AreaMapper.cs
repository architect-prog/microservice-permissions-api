using ArchitectProg.Kernel.Extensions.Mappers;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class AreaMapper : Mapper<AreaEntity, AreaResponse>, IAreaMapper
{
    public override AreaResponse Map(AreaEntity source)
    {
        var result = new AreaResponse(source.Id, source.ApplicationId, source.Name);
        return result;
    }
}