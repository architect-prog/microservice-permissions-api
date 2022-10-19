using Microservice.Permissions.Core.Contracts.Responses.Permission;

namespace Microservice.Permissions.Core.Factories.Interfaces;

public interface IDefaultPermissionFactory
{
    IEnumerable<PermissionResponse> CreateDefaultPermissions();
}