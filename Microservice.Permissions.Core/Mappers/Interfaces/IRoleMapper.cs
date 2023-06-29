using ArchitectProg.Kernel.Extensions.Mappers.Interfaces;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers.Interfaces;

public interface IRoleMapper : IMapper<RoleEntity, RoleResponse>
{
}