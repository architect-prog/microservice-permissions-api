using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public sealed class PermissionCollectionEntity : Entity<int>
{
    public int AreaId { get; set; }
    public AreaEntity Area { get; set; } = null!;
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public ICollection<PermissionEntity> Permissions { get; set; } = null!;
}