namespace Microservice.Permissions.Core.Contracts.Requests.Application
{
    public class CreateApplicationRequest
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}