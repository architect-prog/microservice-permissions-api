using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Services.Interfaces
{
   public interface IPermissionService
   {
      Task<IEnumerable<PermissionsResponse>> GetAll(int[]? roleIds, int[]? areaIds);
   }
}