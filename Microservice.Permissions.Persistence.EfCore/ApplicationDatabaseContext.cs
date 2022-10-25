using System.Reflection;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Extensions;
using Microservice.Permissions.Persistence.EfCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Microservice.Permissions.Persistence.EfCore;

public sealed class ApplicationDatabaseContext : DbContext
{
    private readonly DatabaseSettings databaseSettings;

    public DbSet<AreaEntity> Areas => Set<AreaEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();
    public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
    public DbSet<PermissionCollectionEntity> PermissionCollections => Set<PermissionCollectionEntity>();

    public ApplicationDatabaseContext(IOptions<DatabaseSettings> databaseSettings)
    {
        this.databaseSettings = databaseSettings.Value;

        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.PopulateApplicationData();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(databaseSettings.ConnectionString)
            .UseSnakeCaseNamingConvention();
    }
}