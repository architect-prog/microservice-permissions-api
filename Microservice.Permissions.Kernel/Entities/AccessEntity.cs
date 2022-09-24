using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public class AccessEntity : Entity<int>
{
    public int AreaId { get; set; }
    public AreaEntity? Area { get; set; }
    public int RoleId { get; set; }
    public RoleEntity? Role { get; set; }
    public int ApplicationId { get; set; }
    public ApplicationEntity? Application { get; set; }
    public IEnumerable<PermissionEntity>? Permissions { get; set; }
}