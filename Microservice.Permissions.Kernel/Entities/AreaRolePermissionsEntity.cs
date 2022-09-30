using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities
{
    public sealed class AreaRolePermissionsEntity : Entity<int>
    {
        public int AreaId { get; set; }
        public AreaEntity? Area { get; set; }
        public int RoleId { get; set; }
        public RoleEntity? Role { get; set; }
        public IEnumerable<PermissionEntity> Permissions { get; set; } = null!;
    }
}