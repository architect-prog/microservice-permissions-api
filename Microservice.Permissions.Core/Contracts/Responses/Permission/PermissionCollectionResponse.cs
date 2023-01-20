namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public sealed record PermissionCollectionResponse(
    int RoleId,
    int AreaId,
    IEnumerable<PermissionResponse> CustomPermissions);