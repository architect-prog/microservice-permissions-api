using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Specifications.Area;

namespace Microservice.Permissions.Core.Services;

public sealed class AreaService : IAreaService
{
    private readonly IAreaCreator areaCreator;
    private readonly IAreaMapper areaMapper;
    private readonly IPermissionCollectionService permissionCollectionService;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<AreaEntity> repository;

    public AreaService(
        IAreaCreator areaCreator,
        IAreaMapper areaMapper,
        IPermissionCollectionService permissionCollectionService,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<AreaEntity> repository)
    {
        this.areaCreator = areaCreator;
        this.areaMapper = areaMapper;
        this.permissionCollectionService = permissionCollectionService;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
    }

    public async Task<Result<AreaResponse>> Create(CreateAreaRequest request)
    {
        var area = areaCreator.Create(request);
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Add(area);
            await permissionCollectionService.CreateForArea(area.Id);
            await transaction.Commit();
        }

        var result = areaMapper.Map(area);
        return result;
    }

    public async Task<Result<AreaResponse>> Get(int areaId)
    {
        var area = await repository.GetOrDefault(areaId);
        if (area is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure<AreaResponse>(nameof(area));
            return failureResult;
        }

        var result = areaMapper.Map(area);
        return result;
    }

    public async Task<Result<IEnumerable<AreaResponse>>> GetAll(int? applicationId, int? skip, int? take)
    {
        var specification = applicationId.HasValue
            ? new ApplicationAreasSpecification(applicationId.Value)
            : SpecificationFactory.AllSpecification<AreaEntity>();
        var areas = await repository.List(specification, skip, take);

        var result = areaMapper.MapCollection(areas).ToArray();
        return result;
    }

    public async Task<Result> Update(int areaId, UpdateAreaRequest request)
    {
        var area = await repository.GetOrDefault(areaId);
        if (area is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(area));
            return failureResult;
        }

        area.Name = request.Name;
        area.ApplicationId = request.ApplicationId;
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Update(area);
            await transaction.Commit();
        }

        return ResultFactory.Success();
    }

    public async Task<Result> Delete(int areaId)
    {
        var area = await repository.GetOrDefault(areaId);
        if (area is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(area));
            return failureResult;
        }

        await repository.Delete(area);
        return ResultFactory.Success();
    }

    public Task<int> Count(int? applicationId)
    {
        var specification = applicationId.HasValue
            ? new ApplicationAreasSpecification(applicationId.Value)
            : SpecificationFactory.AllSpecification<AreaEntity>();

        var result = repository.Count(specification);
        return result;
    }
}