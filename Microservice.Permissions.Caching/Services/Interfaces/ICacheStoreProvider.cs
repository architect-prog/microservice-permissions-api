using StackExchange.Redis;

namespace Microservice.Permissions.Caching.Services.Interfaces;

public interface ICacheStoreProvider : IDisposable
{
    IDatabase Database { get; }
    TimeSpan DefaultExpirationTime { get; }
}