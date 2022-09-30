using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Services.Interfaces
{
   public interface IPermissionCollectionService
   {
      Task<Result<IEnumerable<PermissionCollectionResponse>>> Create(CreatePermissionsRequest request);
      Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds);
   }
}