using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications;
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

        public async Task<Result<(int roleId, int areaId)>> Create(CreatePermissionRequest request)
        {
            var specification = new AreaPermissionsSpecification(request.AreaId);

            var permissions = await repository.List(specification);

            var permissionItem = new PermissionEntity()
            {
                Name = request.Name,
                HaveAccess = false,
            };

            foreach (var permission in permissions)
            {
            }

            return new Result<(int roleId, int areaId)>((1, 1));
        }

        public async Task<IEnumerable<PermissionsResponse>> GetAll(int[]? roleIds, int[]? areaIds)
        {
            var specification = new PermissionsSpecification(roleIds, areaIds);

            var areaPermissions = await repository.List(specification);
            var result = areaPermissionsMapper.MapCollection(areaPermissions);

            return result;
        }

        // public async Task<IEnumerable<PermissionsResponse>> GetAll(string application, string role, string area)
        // {
        //     return new[] {new PermissionsResponse()};
        // }
    }
}