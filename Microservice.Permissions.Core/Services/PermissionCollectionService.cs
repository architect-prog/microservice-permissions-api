using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications.PermissionCollection;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services
{
    public sealed class PermissionCollectionService : IPermissionCollectionService
    {
        private readonly IAreaPermissionsMapper areaPermissionsMapper;
        private readonly IRepository<PermissionCollectionEntity> repository;

        public PermissionCollectionService(
            IAreaPermissionsMapper areaPermissionsMapper,
            IRepository<PermissionCollectionEntity> repository)
        {
            this.areaPermissionsMapper = areaPermissionsMapper;
            this.repository = repository;
        }

        public async Task<Result<IEnumerable<PermissionCollectionResponse>>> Create(CreatePermissionsRequest request)
        {
            var specification = new AreaPermissionCollectionsSpecification(request.AreaId);
            var permissionCollections = await repository.List(specification);

            foreach (var collection in permissionCollections)
            {
                var permissions = request.Permissions.Select(x => new PermissionEntity
                {
                    Name = x.Name,
                    HaveAccess = x.HasAccess,
                    PermissionCollectionId = collection.Id
                });

                //collection.Permissions.AddRange();
            }

            var result = areaPermissionsMapper.MapCollection(permissionCollections).ToArray();
            return result;
        }

        public async Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds)
        {
            var specification = new PermissionCollectionsSpecification(areaIds, roleIds);
            var permissionCollections = await repository.List(specification);

            var result = areaPermissionsMapper.MapCollection(permissionCollections).ToArray();
            return result;
        }

        public async Task<Result<(int roleId, int areaId)>> CreateOrUpdate(UpdatePermissionsRequest request)
        {
            var specification = new AreaPermissionCollectionsSpecification(request.AreaId);
            var permissions = await repository.List(specification);

            foreach (var permission in permissions)
            {
                foreach (var permissionRequest in request.Permissions)
                {
                    permission.Permissions.Add(new PermissionEntity
                    {
                        Name = permissionRequest.Name,
                        HaveAccess = permissionRequest.HasAccess
                    });
                }
            }

            return new Result<(int roleId, int areaId)>((1, 1));
        }


        // public async Task<IEnumerable<PermissionsResponse>> GetAll(string application, string role, string area)
        // {
        //     return new[] {new PermissionsResponse()};
        // }
    }
}