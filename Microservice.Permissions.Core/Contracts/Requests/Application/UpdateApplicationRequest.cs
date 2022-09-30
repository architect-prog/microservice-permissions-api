namespace Microservice.Permissions.Core.Contracts.Requests.Application
{
    public class UpdateApplicationRequest
    {
        public string Name { get; }
        public string Description { get; }

        public UpdateApplicationRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}