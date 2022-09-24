using Microservice.Permissions.Api.Configuration.Interfaces;
using Microservice.Permissions.Api.Constants;
using Microservice.Permissions.Database.Settings;

namespace Microservice.Permissions.Api.Configuration;

public class DatabaseSettingsProvider : IDatabaseSettingsProvider
{
    private readonly IConfiguration configuration;
    private readonly Lazy<DatabaseSettings> databaseSettings;

    public DatabaseSettings DatabaseSettings => databaseSettings.Value;

    public DatabaseSettingsProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
        databaseSettings = new Lazy<DatabaseSettings>(() => GetDatabaseSettings());
    }

    private DatabaseSettings GetDatabaseSettings()
    {
        var section = configuration.GetSection(nameof(DatabaseSettings));
        var result = section.Get<DatabaseSettings>();
        if (result is null)
        {
            var message = string.Format(ExceptionConstants.ConfigurationNotFound, nameof(DatabaseSettings));
            throw new InvalidOperationException(message);
        }

        return result;
    }
}