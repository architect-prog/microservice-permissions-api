using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations;

public sealed class AccessEntityTypeConfigurations : IEntityTypeConfiguration<AccessEntity>
{
    public void Configure(EntityTypeBuilder<AccessEntity> builder)
    {
        builder
            .HasOne(x => x.Area)
            .WithMany(x => x.Accesses)
            .HasForeignKey(x => x.AreaId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Application)
            .WithMany(x => x.Accesses)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Accesses)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}