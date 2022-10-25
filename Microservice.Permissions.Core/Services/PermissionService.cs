using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Extensions;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Specifications.Permission;

namespace Microservice.Permissions.Core.Services;

public sealed class PermissionService : IPermissionService
{
    private readonly IPermissionCreator permissionCreator;
    private readonly IPermissionCollectionMapper permissionCollectionMapper;
    private readonly IPermissionCollectionDetailsMapper permissionCollectionDetailsMapper;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<PermissionCollectionEntity> repository;
    private readonly IRepository<PermissionEntity> permissionRepository;

    public PermissionService(
        IPermissionCreator permissionCreator,
        IPermissionCollectionMapper permissionCollectionMapper,
        IPermissionCollectionDetailsMapper permissionCollectionDetailsMapper,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<PermissionCollectionEntity> repository,
        IRepository<PermissionEntity> permissionRepository)
    {
        this.permissionCreator = permissionCreator;
        this.permissionCollectionMapper = permissionCollectionMapper;
        this.permissionCollectionDetailsMapper = permissionCollectionDetailsMapper;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
        this.permissionRepository = permissionRepository;
    }

    public async Task<Result<PermissionCollectionDetailsResponse>> Get(string application, string area, string role)
    {
        var specification = new PermissionCollectionSpecification(application, area, role);
        var permissionCollection = await repository.GetOrDefault(specification);
        if (permissionCollection is null)
        {
            var failureResult = ResultFactory
                .ResourceNotFoundFailure<PermissionCollectionDetailsResponse>(nameof(permissionCollection));
            return failureResult;
        }

        var result = permissionCollectionDetailsMapper.Map(permissionCollection);
        return result;
    }

    public async Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds)
    {
        var specification = new PermissionCollectionsSpecification(areaIds, roleIds);
        var permissionCollections = await repository.List(specification);

        var result = permissionCollectionMapper.MapCollection(permissionCollections).ToArray();
        return result;
    }

    public async Task<Result> Update(UpdatePermissionsRequest request)
    {
        var specification = new AreaPermissionCollectionsSpecification(request.AreaId, request.Names);
        var permissionCollections = await repository.List(specification);

        var rolePermissionCollections = permissionCollections.Where(x => x.RoleId == request.RoleId).ToArray();
        var otherPermissionCollections = permissionCollections.Where(x => x.RoleId != request.RoleId).ToArray();

        if (!rolePermissionCollections.Any())
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(rolePermissionCollections));
            return failureResult;
        }

        var rolePermissions = rolePermissionCollections
            .SelectMany(x => x.Permissions)
            .ToArray();

        var existingPermissions = request.Permissions
            .Where(x => rolePermissions.Any(y => x.Match(y)))
            .ToArray();

        foreach (var rolePermission in rolePermissions)
        {
            var updateRequest = existingPermissions.FirstOrDefault(y => y.Match(rolePermission));
            if (updateRequest is not null)
                rolePermission.HaveAccess = updateRequest.HasAccess;
        }

        var notExistingPermissions = request.Permissions
            .Except(existingPermissions)
            .ToArray();

        var restrictAccessPermissions = notExistingPermissions
            .Select(x => new PermissionRequest(x.Name, false))
            .ToArray();

        var createdOtherRolesPermissions = otherPermissionCollections
            .Select(x => x.Id)
            .SelectMany(x => permissionCreator.Create(x, restrictAccessPermissions))
            .ToArray();

        var createdRolePermissions = rolePermissionCollections
            .Select(x => x.Id)
            .SelectMany(x => permissionCreator.Create(x, notExistingPermissions))
            .ToArray();

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await permissionRepository.UpdateRange(rolePermissions);
            await permissionRepository.AddRange(createdRolePermissions);
            await permissionRepository.AddRange(createdOtherRolesPermissions);
            await transaction.Commit();
        }

        return ResultFactory.Success();
    }

    public async Task<Result> Delete(int areaId, string[] permissions)
    {
        var specification = new AreaPermissionCollectionsSpecification(areaId, permissions);
        var permissionCollections = await repository.List(specification);

        var deletedPermissions = permissionCollections
            .SelectMany(x => x.Permissions)
            .ToArray();

        if (!deletedPermissions.Any())
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(permissions));
            return failureResult;
        }

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await permissionRepository.DeleteRange(deletedPermissions);
            await transaction.Commit();
        }

        return ResultFactory.Success();
    }
}