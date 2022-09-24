namespace Microservice.Permissions.Core.Contracts.Responses.Permission;

public class PermissionResponse
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public bool HaveAccess { get; set; }
}