namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public sealed class PermissionCollectionResponse
{
    public int RoleId { get; init; }
    public int AreaId { get; init; }

    public IEnumerable<PermissionResponse>? CustomPermissions { get; init; }
}