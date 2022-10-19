namespace Microservice.Permissions.Core.Contracts.Requests.Area;

public sealed record CreateAreaRequest(int ApplicationId, string Name);