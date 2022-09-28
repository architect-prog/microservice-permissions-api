namespace Microservice.Permissions.Core.Contracts.Requests.Area
{
    public class UpdateAreaRequest
    {
        public int ApplicationId { get; init; }
        public string Name { get; init; }
    }
}