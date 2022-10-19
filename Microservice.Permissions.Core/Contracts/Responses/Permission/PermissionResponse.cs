namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public sealed record PermissionResponse(string Name, bool HaveAccess, bool IsDefault);
