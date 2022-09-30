using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications.Permission;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAreaPermissionsMapper areaPermissionsMapper;
        private readonly IRepository<AreaRolePermissionsEntity> repository;

        public PermissionService(
            IAreaPermissionsMapper areaPermissionsMapper,
            IRepository<AreaRolePermissionsEntity> repository)
        {
            this.areaPermissionsMapper = areaPermissionsMapper;
            this.repository = repository;
        }

        public async Task<IEnumerable<PermissionCollectionResponse>> GetAll(int[]? areaIds, int[]? roleIds)
        {
            var specification = new PermissionsSpecification(areaIds, roleIds);

            var areaPermissions = await repository.List(specification);
            var result = areaPermissionsMapper.MapCollection(areaPermissions);

            return result;
        }

        public async Task<Result<(int roleId, int areaId)>> CreateOrUpdate(UpdatePermissionCollectionRequest request)
        {
            // var specification = new PermissionsSpecification();
            //
            // var permissions = await repository.List(specification);
            //
            // var permissionItem = new PermissionEntity()
            // {
            //     Name = request.Name,
            //     HaveAccess = false,
            // };
            //
            // foreach (var permission in permissions)
            // {
            // }

            return new Result<(int roleId, int areaId)>((1, 1));
        }

        // public async Task<IEnumerable<PermissionsResponse>> GetAll(string application, string role, string area)
        // {
        //     return new[] {new PermissionsResponse()};
        // }
    }
}