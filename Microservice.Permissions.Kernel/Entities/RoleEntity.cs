using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities
{
    public sealed class RoleEntity : Entity<int>
    {
        public string? Name { get; set; }

        public ICollection<PermissionCollectionEntity> AreaRolePermissions { get; set; } = null!;
    }
}