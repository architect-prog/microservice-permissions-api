namespace Microservice.Permissions.Core.Contracts.Requests.Role;

public sealed class CreateRoleRequest
{
    public string Name { get; }

    public CreateRoleRequest(string name)
    {
        Name = name;
    }
}