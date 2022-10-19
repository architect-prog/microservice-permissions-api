namespace Microservice.Permissions.Core.Contracts.Requests.Application;

public sealed record UpdateApplicationRequest(string Name, string? Description);