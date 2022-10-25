﻿// <auto-generated />
using Microservice.Permissions.Database;
using Microservice.Permissions.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20221001100938_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.ApplicationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_applications");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_applications_name");

                    b.ToTable("applications", (string)null);
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.AreaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_areas");

                    b.HasIndex("ApplicationId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_areas_application_id_name");

                    b.ToTable("areas", (string)null);
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.PermissionCollectionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("integer")
                        .HasColumnName("area_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_permission_collections");

                    b.HasIndex("AreaId")
                        .HasDatabaseName("ix_permission_collections_area_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_permission_collections_role_id");

                    b.ToTable("permission_collections", (string)null);
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("HaveAccess")
                        .HasColumnType("boolean")
                        .HasColumnName("have_access");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("name");

                    b.Property<int>("PermissionCollectionId")
                        .HasColumnType("integer")
                        .HasColumnName("permission_collection_id");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.HasIndex("PermissionCollectionId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_permissions_permission_collection_id_name");

                    b.ToTable("permissions", (string)null);
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_roles_name");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.AreaEntity", b =>
                {
                    b.HasOne("Microservice.Permissions.Kernel.Entities.ApplicationEntity", "Application")
                        .WithMany("Areas")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_areas_applications_application_id");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.PermissionCollectionEntity", b =>
                {
                    b.HasOne("Microservice.Permissions.Kernel.Entities.AreaEntity", "Area")
                        .WithMany("AreaRolePermissions")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_collections_areas_area_id");

                    b.HasOne("Microservice.Permissions.Kernel.Entities.RoleEntity", "Role")
                        .WithMany("AreaRolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_collections_roles_role_id");

                    b.Navigation("Area");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.PermissionEntity", b =>
                {
                    b.HasOne("Microservice.Permissions.Kernel.Entities.PermissionCollectionEntity", "PermissionCollection")
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionCollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permissions_permission_collections_permission_collection_id");

                    b.Navigation("PermissionCollection");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.ApplicationEntity", b =>
                {
                    b.Navigation("Areas");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.AreaEntity", b =>
                {
                    b.Navigation("AreaRolePermissions");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.PermissionCollectionEntity", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Microservice.Permissions.Kernel.Entities.RoleEntity", b =>
                {
                    b.Navigation("AreaRolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
