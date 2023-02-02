using Microservice.Permissions.Caching.Services.Interfaces;
using Microservice.Permissions.Caching.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Microservice.Permissions.Caching.Services;

public class CachingService : ICachingService
{
    private readonly CachingSettings cachingSettings;
    private readonly Lazy<IConnectionMultiplexer> connection;
    private readonly Lazy<IDatabase> database;

    private IConnectionMultiplexer Connection => connection.Value;
    private IDatabase Database => database.Value;

    public CachingService(IOptions<CachingSettings> cachingSettings)
    {
        this.cachingSettings = cachingSettings.Value;

        connection = new Lazy<IConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect(this.cachingSettings.ConnectionString),
            LazyThreadSafetyMode.ExecutionAndPublication);

        database = new Lazy<IDatabase>(
            () => Connection.GetDatabase(),
            LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public async Task SetValue<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        var expiry = expirationTime ?? TimeSpan.FromSeconds(cachingSettings.DefaultExpirationTime);

        await Database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetValueOrDefault<T>(string key)
    {
        var cachedValue = await Database.StringGetAsync(key);
        if (cachedValue.IsNullOrEmpty)
            return default;

        var result = JsonConvert.DeserializeObject<T>(cachedValue.ToString());
        return result;
    }

    public async Task DeleteKey(string key)
    {
        await Database.KeyDeleteAsync(key);
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}