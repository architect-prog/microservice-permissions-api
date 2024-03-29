﻿using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public sealed class PermissionCollectionService : IPermissionCollectionService
{
    private readonly IPermissionCollectionCreator permissionCollectionCreator;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<PermissionCollectionEntity> repository;
    private readonly IRepository<AreaEntity> areaRepository;
    private readonly IRepository<RoleEntity> roleRepository;

    public PermissionCollectionService(
        IPermissionCollectionCreator permissionCollectionCreator,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<PermissionCollectionEntity> repository,
        IRepository<AreaEntity> areaRepository,
        IRepository<RoleEntity> roleRepository)
    {
        this.permissionCollectionCreator = permissionCollectionCreator;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
        this.areaRepository = areaRepository;
        this.roleRepository = roleRepository;
    }

    public async Task<Result<IEnumerable<int>>> CreateForRole(int roleId)
    {
        var role = await roleRepository.GetOrDefault(roleId);
        var areas = await areaRepository.List(SpecificationFactory.AllSpecification<AreaEntity>());

        if (role is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure<IEnumerable<int>>(nameof(role));
            return failureResult;
        }

        var areaIds = areas.Select(x => x.Id);
        var permissionCollections = permissionCollectionCreator
            .CreateForRole(role.Id, areaIds)
            .ToArray();

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.AddRange(permissionCollections);
            await transaction.Commit();
        }

        var result = permissionCollections.Select(x => x.Id).ToArray();
        return result;
    }

    public async Task<Result<IEnumerable<int>>> CreateForArea(int areaId)
    {
        var area = await areaRepository.GetOrDefault(areaId);
        var roles = await roleRepository.List(SpecificationFactory.AllSpecification<RoleEntity>());

        if (area is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure<IEnumerable<int>>(nameof(area));
            return failureResult;
        }

        var roleIds = roles.Select(x => x.Id);
        var permissionCollections = permissionCollectionCreator
            .CreateForArea(areaId, roleIds)
            .ToArray();

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.AddRange(permissionCollections);
            await transaction.Commit();
        }

        var result = permissionCollections.Select(x => x.Id).ToArray();
        return result;
    }
}