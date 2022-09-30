using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities
{
    public class ApplicationEntity : Entity<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<AreaEntity> Areas { get; set; } = null!;
    }
}