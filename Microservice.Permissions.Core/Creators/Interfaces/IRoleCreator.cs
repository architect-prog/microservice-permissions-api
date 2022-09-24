using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces;

public interface IRoleCreator
{
    RoleEntity Create(CreateRoleRequest request);
}