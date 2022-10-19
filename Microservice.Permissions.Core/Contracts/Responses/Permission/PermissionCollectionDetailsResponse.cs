namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public sealed record PermissionCollectionDetailsResponse(
    bool CanCreate,
    bool CanRead,
    bool CanUpdate,
    bool CanDelete,
    IEnumerable<PermissionResponse> CustomPermissions);