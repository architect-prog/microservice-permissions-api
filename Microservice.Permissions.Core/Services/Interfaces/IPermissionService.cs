using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Services.Interfaces;

public interface IPermissionService
{
   Task<Result<PermissionCollectionDetailsResponse>> Get(string application, string area, string role);
   Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds);
   Task<Result> Update(UpdatePermissionsRequest request);
   Task<Result> Delete(int areaId, string[] permissions);
}