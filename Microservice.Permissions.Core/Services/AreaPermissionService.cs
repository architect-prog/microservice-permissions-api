using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Database.Specifications;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Kernel.Extensions;

namespace Microservice.Permissions.Core.Services;

public class AreaPermissionService : IAreaPermissionService
{
    private readonly IRoleService roleService;
    private readonly IAreaService areaService;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IAreaPermissionsMapper areaPermissionsMapper;
    private readonly IRepository<AreaRolePermissionsEntity> repository;

    public AreaPermissionService(
        IRoleService roleService,
        IAreaService areaService,
        IUnitOfWorkFactory unitOfWorkFactory,
        IAreaPermissionsMapper areaPermissionsMapper,
        IRepository<AreaRolePermissionsEntity> repository)
    {
        this.roleService = roleService;
        this.areaService = areaService;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.areaPermissionsMapper = areaPermissionsMapper;
        this.repository = repository;
    }

    public async Task<IEnumerable<AreaPermissionsResponse>> GetAreaPermissions(int areaId, int? roleId)
    {
        var specification = roleId.HasValue
            ? new AreaRolePermissionsSpecification(areaId, roleId.Value).AsSpecification()
            : new AreaPermissionsSpecification(areaId).AsSpecification();

        var areaPermissions = await repository.List(specification);
        var result = areaPermissionsMapper.MapCollection(areaPermissions);

        return result;
    }

    public async Task<IEnumerable<AreaPermissionsResponse>> CreateAreaPermissions(
        int areaId,
        CreatePermissionRequest request)
    {
        var roles = await roleService.GetAll();
        var areaResult = await areaService.Get(areaId);

        var a = roles.Select(x => new AreaRolePermissionsEntity
        {
            RoleId = x.Id,
            AreaId = areaResult.ValueOrDefault.Id,
            Permissions = new[]
            {
                new PermissionEntity
                {
                    Name = request.Name,
                    HaveAccess = false
                }
            }
        });

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.AddRange(a);
        }

        return null;
    }
}