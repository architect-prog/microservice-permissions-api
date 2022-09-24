using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public class PermissionEntity : Entity<int>
{
    public string? Name { get; set; }
    public bool HaveAccess { get; set; }

    public int AccessId { get; set; }
    public AccessEntity Access { get; set; }
}