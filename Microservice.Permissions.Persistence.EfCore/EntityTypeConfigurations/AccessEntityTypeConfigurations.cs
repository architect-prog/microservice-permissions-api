using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations;

public sealed class AccessEntityTypeConfigurations : IEntityTypeConfiguration<AreaRolePermissionsEntity>
{
    public void Configure(EntityTypeBuilder<AreaRolePermissionsEntity> builder)
    {
        builder
            .HasOne(x => x.Area)
            .WithMany(x => x.AreaRolePermissions)
            .HasForeignKey(x => x.AreaId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.AreaRolePermissions)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}