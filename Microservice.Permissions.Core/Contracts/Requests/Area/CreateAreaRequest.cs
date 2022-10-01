namespace Microservice.Permissions.Core.Contracts.Requests.Area;

public sealed class CreateAreaRequest
{
    public int ApplicationId { get; }
    public string Name { get; }

    public CreateAreaRequest(int applicationId, string name)
    {
        ApplicationId = applicationId;
        Name = name;
    }
}