namespace Microservice.Permissions.Core.Contracts.Responses.Permission
{
    public class PermissionResponse
    {
        public string? Name { get; init; }
        public bool HaveAccess { get; init; }
    }
}