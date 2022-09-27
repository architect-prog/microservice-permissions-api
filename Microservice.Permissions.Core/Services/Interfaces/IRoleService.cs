using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;

namespace Microservice.Permissions.Core.Services.Interfaces;

public interface IRoleService
{
    Task<int> Create(CreateRoleRequest request);
    Task<Result<RoleResponse>> Get(int roleId);
    Task<IEnumerable<RoleResponse>> GetAll(int? skip = null, int? take = null);
    Task<Result> Update(int roleId, UpdateRoleRequest request);
    Task<Result> Delete(int roleId);
    Task<int> Count();
}