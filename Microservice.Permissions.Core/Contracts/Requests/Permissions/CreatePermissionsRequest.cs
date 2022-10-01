namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public sealed class CreatePermissionsRequest
    {
        public int AreaId { get; }
        public int RoleId { get; }
        public IEnumerable<PermissionRequest> Permissions { get; }

        public CreatePermissionsRequest(int roleId, int areaId, IEnumerable<PermissionRequest> permissions)
        {
            Permissions = permissions;
            RoleId = roleId;
            AreaId = areaId;
        }
    }
}