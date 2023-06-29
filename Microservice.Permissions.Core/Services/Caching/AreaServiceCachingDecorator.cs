using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Utils;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services.Caching;

public sealed class AreaServiceCachingDecorator : IAreaService
{
    private readonly IAreaService areaService;
    private readonly ICacheService cacheService;

    public AreaServiceCachingDecorator(
        IAreaService areaService,
        ICacheService cacheService)
    {
        this.areaService = areaService;
        this.cacheService = cacheService;
    }

    public Task<Result<AreaResponse>> Create(CreateAreaRequest request)
    {
        return areaService.Create(request);
    }

    public async Task<Result<AreaResponse>> Get(int areaId)
    {
        var key = string.Format(CachingKeys.Area, areaId);
        var cachedResult = await cacheService.GetValueOrDefault<AreaResponse>(key);
        if (cachedResult is not null)
            return cachedResult;

        var result = await areaService.Get(areaId);
        if (result.IsSuccess)
            await cacheService.SetValue(key, result.ValueOrDefault, TimeSpan.FromSeconds(120));

        return result;
    }

    public async Task<Result<IEnumerable<AreaResponse>>> GetAll(int? applicationId, int? skip, int? take)
    {
        var key = string.Format(CachingKeys.Areas, applicationId);
        var cachedResult = await cacheService.GetValueOrDefault<AreaResponse[]>(key);
        if (cachedResult is not null)
            return cachedResult;

        var result = await areaService.GetAll(applicationId, skip, take);
        if (result.IsSuccess)
            await cacheService.SetValue(key, result.ValueOrDefault, TimeSpan.FromSeconds(120));

        return result;
    }

    public Task<Result> Update(int areaId, UpdateAreaRequest request)
    {
        return areaService.Update(areaId, request);
    }

    public Task<Result> Delete(int areaId)
    {
        return areaService.Delete(areaId);
    }

    public Task<int> Count(int? applicationId)
    {
        return areaService.Count(applicationId);
    }
}