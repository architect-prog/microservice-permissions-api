using Microservice.Permissions.Caching.Services.Interfaces;
using Microservice.Permissions.Caching.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Microservice.Permissions.Caching.Services;

public sealed class CacheStoreProvider : ICacheStoreProvider
{
    private readonly CacheSettings cacheSettings;
    private readonly IConnectionMultiplexer connection;

    private readonly Lazy<IDatabase> database;

    public IDatabase Database => database.Value;
    public TimeSpan DefaultExpirationTime => TimeSpan.FromSeconds(cacheSettings.DefaultExpirationTime);

    public CacheStoreProvider(IOptions<CacheSettings> cacheSettings)
    {
        this.cacheSettings = cacheSettings.Value;

        connection = ConnectionMultiplexer.Connect(this.cacheSettings.ConnectionString);
        database = new Lazy<IDatabase>(GetDatabase, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    private IDatabase GetDatabase()
    {
        var result = connection.GetDatabase();
        return result;
    }

    public void Dispose()
    {
        connection.Dispose();
    }
}