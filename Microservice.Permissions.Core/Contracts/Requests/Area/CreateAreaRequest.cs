namespace Microservice.Permissions.Core.Contracts.Requests.Area;

public class CreateAreaRequest
{
    public int ApplicationId { get; init; }
    public string? Name { get; init; }
}