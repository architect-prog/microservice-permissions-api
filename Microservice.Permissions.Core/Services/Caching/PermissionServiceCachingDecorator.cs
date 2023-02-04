using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Caching.Services.Interfaces;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Extensions;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services.Caching;

public sealed class PermissionServiceCachingDecorator : IPermissionService
{
    private readonly ICacheService cacheService;
    private readonly IPermissionService permissionService;

    public PermissionServiceCachingDecorator(
        ICacheService cacheService,
        IPermissionService permissionService)
    {
        this.cacheService = cacheService;
        this.permissionService = permissionService;
    }

    public async Task<Result<PermissionCollectionDetailsResponse>> Get(string application, string area, string role)
    {
        var key = string.Format(CachingKeys.ApplicationAreaRolePermissions, application, area, role);
        var cachedResult = await cacheService.GetValueOrDefault<PermissionCollectionDetailsResponse>(key);
        if (cachedResult is not null)
            return cachedResult;

        var result = await permissionService.Get(application, area, role);
        if (result.IsSuccess)
            await cacheService.SetValue(key, result.ValueOrDefault, TimeSpan.FromSeconds(600));

        return result;
    }

    public async Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds)
    {
        var key = string.Format(
            CachingKeys.AreasRolesPermissions,
            areaIds.ToStringSequence(),
            roleIds.ToStringSequence());

        var cachedResult = await cacheService.GetValueOrDefault<PermissionCollectionResponse[]>(key);
        if (cachedResult is not null)
            return cachedResult;

        var result = await permissionService.GetAll(areaIds, roleIds);
        if (result.IsSuccess)
            await cacheService.SetValue(key, result.ValueOrDefault, TimeSpan.FromSeconds(120));

        return result;
    }

    public Task<Result> Update(UpdatePermissionsRequest request)
    {
        return permissionService.Update(request);
    }

    public Task<Result> Delete(int areaId, string[] permissions)
    {
        return permissionService.Delete(areaId, permissions);
    }
}