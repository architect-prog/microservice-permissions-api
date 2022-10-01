namespace Microservice.Permissions.Core.Contracts.Responses.Area;

public sealed class AreaResponse
{
    public int Id { get; init; }
    public int ApplicationId { get; init; }
    public string? Name { get; init; }
}