using ArchitectProg.Kernel.Extensions.Factories.Interfaces;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Utils;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public sealed class RoleService : IRoleService
{
    private readonly IResultFactory resultFactory;
    private readonly ISpecificationFactory specificationFactory;
    private readonly IRoleCreator roleCreator;
    private readonly IRoleMapper roleMapper;
    private readonly IPermissionCollectionService permissionCollectionService;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<RoleEntity> repository;

    public RoleService(
        IResultFactory resultFactory,
        ISpecificationFactory specificationFactory,
        IRoleCreator roleCreator,
        IRoleMapper roleMapper,
        IPermissionCollectionService permissionCollectionService,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<RoleEntity> repository)
    {
        this.resultFactory = resultFactory;
        this.specificationFactory = specificationFactory;
        this.roleCreator = roleCreator;
        this.roleMapper = roleMapper;
        this.permissionCollectionService = permissionCollectionService;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
    }

    public async Task<Result<RoleResponse>> Create(CreateRoleRequest request)
    {
        var role = roleCreator.Create(request);
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Add(role);
            await permissionCollectionService.CreateForRole(role.Id);
            await transaction.Commit();
        }

        var result = roleMapper.Map(role);
        return result;
    }

    public async Task<Result<RoleResponse>> Get(int roleId)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure<RoleResponse>(nameof(role));
            return failureResult;
        }

        var result = roleMapper.Map(role);
        return result;
    }

    public async Task<Result<IEnumerable<RoleResponse>>> GetAll(int? skip = null, int? take = null)
    {
        var roles = await repository
            .List(specificationFactory.AllSpecification<RoleEntity>(), skip, take);

        var result = roleMapper.MapCollection(roles).ToArray();
        return result;
    }

    public async Task<Result> Update(int roleId, UpdateRoleRequest request)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(nameof(role));
            return failureResult;
        }

        role.Name = request.Name;
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Update(role);
            await transaction.Commit();
        }

        return resultFactory.Success();
    }

    public async Task<Result> Delete(int roleId)
    {
        var role = await repository.GetOrDefault(roleId);
        if (role is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(nameof(role));
            return failureResult;
        }

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Delete(role);
            await transaction.Commit();
        }

        return resultFactory.Success();
    }

    public Task<int> Count()
    {
        var result = repository.Count(specificationFactory.AllSpecification<RoleEntity>());
        return result;
    }
}