using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Extensions;

public static class DataPopulationExtensions
{
    private const int AppId1 = 1;
    private const int AppId2 = 2;

    private const int AreaId1 = 1;
    private const int AreaId2 = 2;
    private const int AreaId3 = 3;
    private const int AreaId4 = 4;

    private const int RoleId1 = 1;
    private const int RoleId2 = 2;

    private const int PermissionCollectionId1 = 1;
    private const int PermissionCollectionId2 = 2;
    private const int PermissionCollectionId3 = 3;
    private const int PermissionCollectionId4 = 4;
    private const int PermissionCollectionId5 = 5;
    private const int PermissionCollectionId6 = 6;
    private const int PermissionCollectionId7 = 7;
    private const int PermissionCollectionId8 = 8;

    private const int PermissionId1 = 1;
    private const int PermissionId2 = 2;

    private const string Description =
        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
        "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
        "when an unknown printer took a galley of type and scrambled it to make a type specimen book. " +
        "It has survived not only five centuries, but also the leap into electronic typesetting, " +
        "remaining essentially unchanged. " +
        "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
        "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

    private static readonly ApplicationEntity[] applications =
    {
        new()
        {
            Id = AppId1,
            Name = "microservice-messaging",
            Description = Description
        },
        new()
        {
            Id = AppId2,
            Name = "microservice-permissions",
            Description = Description
        }
    };

    private static readonly AreaEntity[] areas =
    {
        new()
        {
            Id = AreaId1,
            ApplicationId = AppId1,
            Name = "plans"
        },
        new()
        {
            Id = AreaId2,
            ApplicationId = AppId1,
            Name = "messages"
        },
        new()
        {
            Id = AreaId3,
            ApplicationId = AppId2,
            Name = "values"
        },
        new()
        {
            Id = AreaId4,
            ApplicationId = AppId2,
            Name = "charts"
        }
    };

    private static readonly RoleEntity[] roles =
    {
        new()
        {
            Id = RoleId1,
            Name = "Client"
        },
        new()
        {
            Id = RoleId2,
            Name = "Administrator"
        }
    };

    private static readonly PermissionCollectionEntity[] permissionCollections =
    {
        new()
        {
            Id = PermissionCollectionId1,
            AreaId = AreaId1,
            RoleId = RoleId1
        },
        new()
        {
            Id = PermissionCollectionId2,
            AreaId = AreaId2,
            RoleId = RoleId1
        },
        new()
        {
            Id = PermissionCollectionId3,
            AreaId = AreaId3,
            RoleId = RoleId1
        },
        new()
        {
            Id = PermissionCollectionId4,
            AreaId = AreaId4,
            RoleId = RoleId1
        },
        new()
        {
            Id = PermissionCollectionId5,
            AreaId = AreaId1,
            RoleId = RoleId2
        },
        new()
        {
            Id = PermissionCollectionId6,
            AreaId = AreaId2,
            RoleId = RoleId2
        },
        new()
        {
            Id = PermissionCollectionId7,
            AreaId = AreaId3,
            RoleId = RoleId2
        },
        new()
        {
            Id = PermissionCollectionId8,
            AreaId = AreaId4,
            RoleId = RoleId2
        }
    };

    private static readonly PermissionEntity[] permissions =
    {
        new()
        {
            Id = PermissionId1,
            PermissionCollectionId = PermissionCollectionId1,
            Name = "can_download",
            HaveAccess = true
        },
        new()
        {
            Id = PermissionId2,
            PermissionCollectionId = PermissionCollectionId5,
            Name = "can_download",
            HaveAccess = false
        }
    };

    public static void PopulateApplicationData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationEntity>().HasData(applications);
        modelBuilder.Entity<AreaEntity>().HasData(areas);
        modelBuilder.Entity<RoleEntity>().HasData(roles);
        modelBuilder.Entity<PermissionCollectionEntity>().HasData(permissionCollections);
        modelBuilder.Entity<PermissionEntity>().HasData(permissions);
    }
}