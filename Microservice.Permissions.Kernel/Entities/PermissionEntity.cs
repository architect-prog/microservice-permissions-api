using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities
{
    public class PermissionEntity : Entity<int>
    {
        public string? Name { get; set; }
        public bool HaveAccess { get; set; }

        public int AreaRolePermissionsId { get; set; }
        public AreaRolePermissionsEntity? AreaRolePermissions { get; set; }
    }
}