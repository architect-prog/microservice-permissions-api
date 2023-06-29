using ArchitectProg.Kernel.Extensions.Entities;

namespace Microservice.Permissions.Kernel.Entities;

public sealed class RoleEntity : Entity<int>
{
    public string Name { get; set; } = null!;
    public ICollection<PermissionCollectionEntity> AreaRolePermissions { get; set; } = null!;
}