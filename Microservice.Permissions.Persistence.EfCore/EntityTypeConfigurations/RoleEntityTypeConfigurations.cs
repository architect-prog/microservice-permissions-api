using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations;

public sealed class RoleEntityTypeConfigurations : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.HasMany(x => x.Accesses).WithOne(x => x.Role).OnDelete(DeleteBehavior.Cascade);
    }
}