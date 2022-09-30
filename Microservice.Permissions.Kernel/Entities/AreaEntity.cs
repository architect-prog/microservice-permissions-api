using ArchitectProg.Kernel.Extensions.Abstractions;

namespace Microservice.Permissions.Kernel.Entities
{
    public sealed class AreaEntity : Entity<int>
    {
        public string? Name { get; set; }

        public int ApplicationId { get; set; }
        public ApplicationEntity? Application { get; set; }

        public IEnumerable<AreaRolePermissionsEntity> AreaRolePermissions { get; set; } = null!;
    }
}