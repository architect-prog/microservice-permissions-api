namespace Microservice.Permissions.Core.Contracts.Requests.Permission;

public sealed record PermissionRequest(string Name, bool HasAccess);