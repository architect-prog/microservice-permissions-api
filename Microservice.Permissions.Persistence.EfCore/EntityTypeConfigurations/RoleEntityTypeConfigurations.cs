﻿using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Persistence.EfCore.EntityTypeConfigurations;

public sealed class RoleEntityTypeConfigurations : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.HasIndex(x => x.Name).IsUnique();

        builder
            .HasMany(x => x.AreaRolePermissions)
            .WithOne(x => x.Role)
            .OnDelete(DeleteBehavior.Cascade);
    }
}