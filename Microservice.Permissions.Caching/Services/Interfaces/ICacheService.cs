namespace Microservice.Permissions.Caching.Services.Interfaces;

public interface ICacheService
{
    Task SetValue<T>(string key, T value, TimeSpan? expirationTime = null);
    Task<T?> GetValueOrDefault<T>(string key);
    Task DeleteKey(string key);
}