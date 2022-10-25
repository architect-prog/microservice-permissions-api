using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Persistence.EfCore.EntityTypeConfigurations;

public sealed class PermissionEntityTypeConfigurations : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(32);
        builder.HasIndex(x => new {AreaRolePermissionsId = x.PermissionCollectionId, x.Name}).IsUnique();
        builder
            .HasOne(x => x.PermissionCollection)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.PermissionCollectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}