using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public class AreaRoleService : IAreaRoleService
{
    private readonly IAreaRoleCreator areaRoleCreator;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<AreaRolePermissionsEntity> repository;
    private readonly IRepository<AreaEntity> areaRepository;
    private readonly IRepository<RoleEntity> roleRepository;

    public AreaRoleService(
        IAreaRoleCreator areaRoleCreator,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<AreaRolePermissionsEntity> repository,
        IRepository<AreaEntity> areaRepository,
        IRepository<RoleEntity> roleRepository)
    {
        this.areaRoleCreator = areaRoleCreator;
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
        var areaRoles = areaRoleCreator
            .CreateForRole(role.Id, areaIds)
            .ToArray();

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.AddRange(areaRoles);
            await transaction.Commit();
        }

        var result = areaRoles.Select(x => x.Id).ToArray();
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
        var areaRoles = areaRoleCreator
            .CreateForArea(areaId, roleIds)
            .ToArray();

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.AddRange(areaRoles);
            await transaction.Commit();
        }

        var result = areaRoles.Select(x => x.Id).ToArray();
        return result;
    }
}