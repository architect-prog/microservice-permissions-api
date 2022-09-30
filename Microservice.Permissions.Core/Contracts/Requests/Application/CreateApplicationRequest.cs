namespace Microservice.Permissions.Core.Contracts.Requests.Application
{
    public class CreateApplicationRequest
    {
        public string Name { get; }
        public string Description { get; }

        public CreateApplicationRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}