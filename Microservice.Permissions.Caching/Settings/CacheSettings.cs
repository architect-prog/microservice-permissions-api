namespace Microservice.Permissions.Caching.Settings;

public sealed class CacheSettings
{
    public string ConnectionString { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
    public long DefaultExpirationTime { get; set; }
}