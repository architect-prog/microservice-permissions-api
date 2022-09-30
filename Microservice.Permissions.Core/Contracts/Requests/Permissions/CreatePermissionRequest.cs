namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public class CreatePermissionRequest
    {
        public int AreaId { get; }
        public string Name { get; }

        public CreatePermissionRequest(int areaId, string name)
        {
            AreaId = areaId;
            Name = name;
        }
    }
}