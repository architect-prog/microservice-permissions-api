using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers.Interfaces
{
    public interface IAreaMapper : IMapper<AreaEntity, AreaResponse>
    {
    }
}