using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations
{
    public sealed class AreaEntityTypeConfigurations : IEntityTypeConfiguration<AreaEntity>
    {
        public void Configure(EntityTypeBuilder<AreaEntity> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(64);
            builder.HasIndex(x => new {x.ApplicationId, x.Name}).IsUnique();
            builder
                .HasOne(x => x.Application)
                .WithMany(x => x.Areas)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}