using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Factories.Interfaces;

namespace Microservice.Permissions.Core.Factories;

public class DefaultPermissionFactory : IDefaultPermissionFactory
{
    public IEnumerable<PermissionResponse> CreateDefaultPermissions()
    {
        var defaultPermissions = PermissionConstants.Defaults.Select(x => new PermissionResponse(x, false, true));
        return defaultPermissions;
    }
}