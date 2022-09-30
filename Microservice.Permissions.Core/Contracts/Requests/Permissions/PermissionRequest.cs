namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public sealed class PermissionRequest
    {
        public string Name { get; }
        public bool HasAccess { get; }

        public PermissionRequest(string name, bool hasAccess)
        {
            Name = name;
            HasAccess = hasAccess;
        }
    }
}