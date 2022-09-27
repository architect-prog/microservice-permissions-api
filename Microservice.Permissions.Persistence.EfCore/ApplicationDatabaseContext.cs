using System.Reflection;
using Microservice.Permissions.Database.Settings;
using Microservice.Permissions.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Microservice.Permissions.Database;

public sealed class ApplicationDatabaseContext : DbContext
{
    private readonly DatabaseSettings databaseSettings;

    public DbSet<AreaEntity> Areas => Set<AreaEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<AreaRolePermissionsEntity> AreaRolePermissions => Set<AreaRolePermissionsEntity>();
    public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();
    public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();

    public ApplicationDatabaseContext(IOptions<DatabaseSettings> databaseSettings)
    {
        this.databaseSettings = databaseSettings.Value;

        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(databaseSettings.ConnectionString)
            .UseSnakeCaseNamingConvention();
    }
}