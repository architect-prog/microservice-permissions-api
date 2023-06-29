using ArchitectProg.Kernel.Extensions.Mappers;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers;

public sealed class RoleMapper : Mapper<RoleEntity, RoleResponse>, IRoleMapper
{
    public override RoleResponse Map(RoleEntity source)
    {
        var result = new RoleResponse(source.Id, source.Name);
        return result;
    }
}