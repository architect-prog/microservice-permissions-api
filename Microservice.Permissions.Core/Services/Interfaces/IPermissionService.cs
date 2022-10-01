using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Services.Interfaces
{
   public interface IPermissionService
   {
      Task<Result<IEnumerable<PermissionCollectionResponse>>> Create(CreatePermissionsRequest request);
      Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds);
      Task<Result> Update(UpdatePermissionsRequest request);
      Task<Result> Delete(int areaId, string[] permission);
   }
}