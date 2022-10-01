namespace Microservice.Permissions.Core.Contracts.Requests.Role;

public sealed class UpdateRoleRequest
{
    public string Name { get; }

    public UpdateRoleRequest(string name)
    {
        Name = name;
    }
}