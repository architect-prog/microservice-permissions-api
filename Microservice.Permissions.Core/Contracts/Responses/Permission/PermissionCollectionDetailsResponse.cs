namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public sealed class PermissionCollectionDetailsResponse
{
    public bool CanCreate { get; init; }
    public bool CanRead { get; init; }
    public bool CanUpdate { get; init; }
    public bool CanDelete { get; init; }

    public IEnumerable<PermissionResponse>? CustomPermissions { get; init; }
}