using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public class RoleService : IRoleService
{
    private readonly IRoleCreator roleCreator;
    private readonly IRoleMapper roleMapper;
    private readonly IAreaRoleService areaRoleService;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<RoleEntity> repository;

    public RoleService(
        IRoleCreator roleCreator,
        IRoleMapper roleMapper,
        IAreaRoleService areaRoleService,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<RoleEntity> repository)
    {
        this.roleCreator = roleCreator;
        this.roleMapper = roleMapper;
        this.areaRoleService = areaRoleService;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
    }

    public async Task<int> Create(CreateRoleRequest request)
    {
        var role = roleCreator.Create(request);
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Add(role);
            await areaRoleService.CreateForRole(role.Id);
            await transaction.Commit();
        }

        var result = role.Id;
        return result;
    }

    public async Task<Result<RoleResponse>> Get(int roleId)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure<RoleResponse>(nameof(role));
            return failureResult;
        }

        var result = roleMapper.Map(role);
        return result;
    }

    public async Task<IEnumerable<RoleResponse>> GetAll(int? skip = null, int? take = null)
    {
        var roles = await repository
            .List(SpecificationFactory.AllSpecification<RoleEntity>(), skip, take);

        var result = roleMapper.MapCollection(roles);
        return result;
    }

    public async Task<Result> Update(int roleId, UpdateRoleRequest request)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(role));
            return failureResult;
        }

        role.Name = request.Name;
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Update(role);
            await transaction.Commit();
        }

        return ResultFactory.Success();
    }

    public async Task<Result> Delete(int roleId)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(role));
            return failureResult;
        }

        await repository.Delete(role);
        return ResultFactory.Success();
    }

    public Task<int> Count()
    {
        var result = repository.Count(SpecificationFactory.AllSpecification<RoleEntity>());
        return result;
    }
}