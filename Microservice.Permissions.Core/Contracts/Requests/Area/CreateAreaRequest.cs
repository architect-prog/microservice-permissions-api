namespace Microservice.Permissions.Core.Contracts.Requests.Area;

public class CreateAreaRequest
{
    public int ApplicationId { get; set; }
    public string? Name { get; init; }
}