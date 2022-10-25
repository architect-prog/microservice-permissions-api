using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Persistence.EfCore.EntityTypeConfigurations;

public sealed class PermissionCollectionEntityTypeConfigurations
    : IEntityTypeConfiguration<PermissionCollectionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionCollectionEntity> builder)
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