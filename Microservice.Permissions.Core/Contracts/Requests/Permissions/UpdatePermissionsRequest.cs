namespace Microservice.Permissions.Core.Contracts.Requests.Permissions;

public sealed class UpdatePermissionsRequest
{
    public int AreaId { get; }
    public int RoleId { get; }
    public IEnumerable<PermissionRequest> Permissions { get; }
    public string[] Names => Permissions.Select(x => x.Name).ToArray();

    public UpdatePermissionsRequest(
        int areaId,
        int roleId,
        IEnumerable<PermissionRequest> permissions)
    {
        AreaId = areaId;
        RoleId = roleId;
        Permissions = permissions;
    }
}