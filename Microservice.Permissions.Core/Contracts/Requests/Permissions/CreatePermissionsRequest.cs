namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public sealed class CreatePermissionsRequest
    {
        public int AreaId { get; }
        public IEnumerable<PermissionRequest> Permissions { get; }

        public CreatePermissionsRequest(int areaId, IEnumerable<PermissionRequest> permissions)
        {
            AreaId = areaId;
            Permissions = permissions;
        }
    }
}