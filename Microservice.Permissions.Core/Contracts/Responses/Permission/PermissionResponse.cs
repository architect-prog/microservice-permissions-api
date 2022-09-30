namespace Microservice.Permissions.Core.Contracts.Responses.Permission
{
    public sealed class PermissionResponse
    {
        public string? Name { get; init; }
        public bool HaveAccess { get; init; }
    }
}