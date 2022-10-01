using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public sealed class ApplicationEntity : Entity<int>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<AreaEntity> Areas { get; set; } = null!;
}