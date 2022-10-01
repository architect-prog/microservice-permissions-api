using ArchitectProg.FunctionalExtensions.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Extensions;

public static class MatchExtensions
{
    public static bool Match(this PermissionRequest request, PermissionEntity entity)
    {
        var result = request.Name.EqualsIgnoreCase(entity.Name);
        return result;
    }

    public static bool Match(this PermissionEntity entity, PermissionRequest request)
    {
        var result = request.Name.EqualsIgnoreCase(entity.Name);
        return result;
    }
}