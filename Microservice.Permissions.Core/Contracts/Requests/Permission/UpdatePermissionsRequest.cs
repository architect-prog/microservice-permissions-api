namespace Microservice.Permissions.Core.Contracts.Requests.Permission;

public sealed record UpdatePermissionsRequest(int AreaId, int RoleId, IEnumerable<PermissionRequest> Permissions)
{
    public string[] Names => Permissions.Select(x => x.Name).ToArray();
}