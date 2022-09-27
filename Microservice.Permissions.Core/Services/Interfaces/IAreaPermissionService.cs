using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Services.Interfaces;

public interface IAreaPermissionService
{
   Task<IEnumerable<AreaPermissionsResponse>> GetAreaPermissions(int areaId, int? roleId);
}