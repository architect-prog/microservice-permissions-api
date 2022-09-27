﻿using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public class AreaService : IAreaService
{
    private readonly IAreaCreator areaCreator;
    private readonly IAreaMapper areaMapper;
    private readonly IAreaRoleService areaRoleService;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<AreaEntity> repository;

    public AreaService(
        IAreaCreator areaCreator,
        IAreaMapper areaMapper,
        IAreaRoleService areaRoleService,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<AreaEntity> repository)
    {
        this.areaCreator = areaCreator;
        this.areaMapper = areaMapper;
        this.areaRoleService = areaRoleService;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
    }

    public async Task<int> Create(CreateAreaRequest request)
    {
        var area = areaCreator.Create(request);
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Add(area);
            await areaRoleService.CreateForArea(area.Id);
            await transaction.Commit();
        }

        var result = area.Id;
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

    public async Task<IEnumerable<AreaResponse>> GetAll(int? applicationId, int? skip, int? take)
    {
        var specification = applicationId.HasValue
            ? new ApplicationAreasSpecification(applicationId.Value)
            : SpecificationFactory.AllSpecification<AreaEntity>();
        var areas = await repository.List(specification, skip, take);

        var result = areaMapper.MapCollection(areas);
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

    public Task<int> Count()
    {
        var result = repository.Count(SpecificationFactory.AllSpecification<AreaEntity>());
        return result;
    }
}