using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications.Permission;
using Microservice.Permissions.Database.Specifications.PermissionCollection;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services
{
    public sealed class PermissionService : IPermissionService
    {
        private readonly IPermissionCreator permissionCreator;
        private readonly IPermissionCollectionMapper permissionCollectionMapper;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IRepository<PermissionCollectionEntity> repository;
        private readonly IRepository<PermissionEntity> permissionRepository;

        public PermissionService(
            IPermissionCreator permissionCreator,
            IPermissionCollectionMapper permissionCollectionMapper,
            IUnitOfWorkFactory unitOfWorkFactory,
            IRepository<PermissionCollectionEntity> repository,
            IRepository<PermissionEntity> permissionRepository)
        {
            this.permissionCreator = permissionCreator;
            this.permissionCollectionMapper = permissionCollectionMapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.repository = repository;
            this.permissionRepository = permissionRepository;
        }

        public async Task<Result<IEnumerable<PermissionCollectionResponse>>> Create(CreatePermissionsRequest request)
        {
            var specification = new AreaPermissionCollectionsSpecification(request.AreaId);
            var permissionCollections = await repository.List(specification);

            var rolePermissions = permissionCollections
                .Where(x => x.RoleId == request.RoleId)
                .Select(x => x.Id)
                .SelectMany(x => permissionCreator.Create(x, request.Permissions))
                .ToArray();

            var restrictAccessPermissions = request.Permissions
                .Select(x => new PermissionRequest(x.Name, false))
                .ToArray();

            var otherRolesPermissions = permissionCollections
                .Where(x => x.RoleId != request.RoleId)
                .Select(x => x.Id)
                .SelectMany(x => permissionCreator.Create(x, restrictAccessPermissions))
                .ToArray();

            using (var transaction = unitOfWorkFactory.BeginTransaction())
            {
                await permissionRepository.AddRange(rolePermissions);
                await permissionRepository.AddRange(otherRolesPermissions);
                await transaction.Commit();
            }

            var result = permissionCollectionMapper.MapCollection(permissionCollections).ToArray();
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
            var specification = new AreaPermissionCollectionsSpecification(request.AreaId);
            var permissionCollections = await repository.List(specification);

            var rolePermissions = permissionCollections.FirstOrDefault(x => x.RoleId == request.RoleId);
            if (rolePermissions is null)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(rolePermissions));
                return failureResult;
            }

            var existingPermissions = request.Permissions
                .Where(x => rolePermissions.Permissions.Any(y => x.Name.ToUpper() == y.Name.ToUpper()));

            var notExistingPermissions = request.Permissions.Except(existingPermissions);

            var restrictAccessPermissions = notExistingPermissions
                .Select(x => new PermissionRequest(x.Name, false))
                .ToArray();

            var otherRolesPermissions = permissionCollections
                .Where(x => x.RoleId != request.RoleId)
                .Select(x => x.Id)
                .SelectMany(x => permissionCreator.Create(x, restrictAccessPermissions))
                .ToArray();

            using (var transaction = unitOfWorkFactory.BeginTransaction())
            {
                await permissionRepository.AddRange(otherRolesPermissions);
                await transaction.Commit();
            }

            return ResultFactory.Success();
        }

        public async Task<Result> Delete(int areaId, string[] permission)
        {
            var specification = new PermissionByAreaSpecification(areaId, permission);
            var permissions = await permissionRepository.List(specification);

            if (!permission.Any())
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(permissions));
                return failureResult;
            }

            using (var transaction = unitOfWorkFactory.BeginTransaction())
            {
                await permissionRepository.DeleteRange(permissions);
                await transaction.Commit();
            }

            return ResultFactory.Success();
        }
    }
}