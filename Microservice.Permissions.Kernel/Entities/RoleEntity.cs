using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public class RoleEntity : Entity<int>
{
    public string? Name { get; set; }

    public IEnumerable<AreaRolePermissionsEntity>? AreaRolePermissions { get; set; }
}