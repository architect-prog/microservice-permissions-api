using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers.Interfaces
{
    public interface IAreaPermissionsMapper : IMapper<AreaRolePermissionsEntity, PermissionsResponse>
    {
    }
}