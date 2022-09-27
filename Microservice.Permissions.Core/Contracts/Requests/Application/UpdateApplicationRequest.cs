namespace Microservice.Permissions.Core.Contracts.Requests.Application;

public class UpdateApplicationRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}