using Microservice.Permissions.Database.Settings;

namespace Microservice.Permissions.Api.Configuration.Interfaces;

public interface IDatabaseSettingsProvider
{
    DatabaseSettings DatabaseSettings { get; }
}