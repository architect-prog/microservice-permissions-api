using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities;

public class AreaEntity : Entity<int>
{
    public string? Name { get; set; }

    public int ApplicationId { get; set; }
    public ApplicationEntity? Application { get; set; }
    public IEnumerable<AccessEntity>? Accesses { get; set; }
}