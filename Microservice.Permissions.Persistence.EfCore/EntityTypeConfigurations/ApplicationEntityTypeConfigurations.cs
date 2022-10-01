using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations;

public sealed class ApplicationEntityTypeConfigurations : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.HasIndex(x => x.Name).IsUnique();
    }
}