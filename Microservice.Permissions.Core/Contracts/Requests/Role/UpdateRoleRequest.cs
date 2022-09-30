namespace Microservice.Permissions.Core.Contracts.Requests.Role
{
    public class UpdateRoleRequest
    {
        public string Name { get; }

        public UpdateRoleRequest(string name)
        {
            Name = name;
        }
    }
}