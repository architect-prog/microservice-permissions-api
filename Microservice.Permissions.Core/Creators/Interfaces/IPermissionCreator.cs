using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces;

public interface IPermissionCreator
{
    IEnumerable<PermissionEntity> Create(int permissionCollectionId, IEnumerable<PermissionRequest> request);
    PermissionEntity Create(int permissionCollectionId, PermissionRequest request);
}