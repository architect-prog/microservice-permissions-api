using ArchitectProg.Kernel.Extensions.Entities;

namespace Microservice.Permissions.Kernel.Entities;

public sealed class AreaEntity : Entity<int>
{
    public string Name { get; set; } = null!;
    public int ApplicationId { get; set; }
    public ApplicationEntity Application { get; set; } = null!;
    public ICollection<PermissionCollectionEntity> AreaRolePermissions { get; set; } = null!;
}