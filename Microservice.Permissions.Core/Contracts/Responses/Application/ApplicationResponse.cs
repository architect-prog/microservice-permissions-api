namespace Microservice.Permissions.Core.Contracts.Responses.Application
{
    public sealed class ApplicationResponse
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}