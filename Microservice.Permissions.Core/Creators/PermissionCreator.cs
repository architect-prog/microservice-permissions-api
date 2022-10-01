using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators;

public sealed class PermissionCreator : IPermissionCreator
{
    public IEnumerable<PermissionEntity> Create(
        int permissionCollectionId,
        IEnumerable<PermissionRequest> request)
    {
        var result = request.Select(x => Create(permissionCollectionId, x));
        return result;
    }

    public PermissionEntity Create(int permissionCollectionId, PermissionRequest request)
    {
        var result = new PermissionEntity
        {
            Name = request.Name,
            HaveAccess = request.HasAccess,
            PermissionCollectionId = permissionCollectionId
        };

        return result;
    }
}