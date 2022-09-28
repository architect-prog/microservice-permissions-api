namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public class CreatePermissionRequest
    {
        public int AreaId { get; init; }
        public string? Name { get; set; }
    }
}