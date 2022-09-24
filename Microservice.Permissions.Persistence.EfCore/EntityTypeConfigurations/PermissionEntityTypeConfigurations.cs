﻿using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Permissions.Database.EntityTypeConfigurations;

public sealed class PermissionEntityTypeConfigurations : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(32);
        builder
            .HasOne(x => x.Access)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.AccessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}