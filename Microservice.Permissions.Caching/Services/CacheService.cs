using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Caching.Services.Interfaces;
using Newtonsoft.Json;

namespace Microservice.Permissions.Caching.Services;

public sealed class CacheService : ICacheService
{
    private readonly ICacheStoreProvider cacheStoreProvider;

    public CacheService(ICacheStoreProvider cacheStoreProvider)
    {
        this.cacheStoreProvider = cacheStoreProvider;
    }

    public async Task SetValue<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        var expiry = expirationTime ?? cacheStoreProvider.DefaultExpirationTime;

        await cacheStoreProvider.Database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetValueOrDefault<T>(string key)
    {
        var cachedValue = await cacheStoreProvider.Database.StringGetAsync(key);
        if (cachedValue.IsNullOrEmpty)
            return default;

        var result = JsonConvert.DeserializeObject<T>(cachedValue.ToString());
        return result;
    }

    public async Task DeleteKey(string key)
    {
        await cacheStoreProvider.Database.KeyDeleteAsync(key);
    }
}