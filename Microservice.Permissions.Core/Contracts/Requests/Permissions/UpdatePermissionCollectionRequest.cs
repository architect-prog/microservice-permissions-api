namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public class UpdatePermissionCollectionRequest
    {
        public int AreaId { get; }
        public int RoleId { get; }
        public IEnumerable<UpdatePermissionRequest> Permissions { get; }

        public UpdatePermissionCollectionRequest(
            int areaId,
            int roleId,
            IEnumerable<UpdatePermissionRequest> permissions)
        {
            AreaId = areaId;
            RoleId = roleId;
            Permissions = permissions;
        }
    }
}