namespace Microservice.Permissions.Caching.Settings;

public sealed class CachingSettings
{
    public string ConnectionString { get; set; } = null!;
    public long DefaultExpirationTime { get; set; }
}