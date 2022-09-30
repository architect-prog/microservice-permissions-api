namespace Microservice.Permissions.Core.Contracts.Requests.Area
{
    public class CreateAreaRequest
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public CreateAreaRequest(int applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}