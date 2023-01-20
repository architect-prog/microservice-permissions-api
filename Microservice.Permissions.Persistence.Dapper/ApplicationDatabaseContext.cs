using System.Data;
using Microservice.Permissions.Persistence.Dapper.Settings;
using Npgsql;

namespace Microservice.Permissions.Persistence.Dapper;

public class ApplicationDatabaseContext : IDisposable, IAsyncDisposable
{
    private readonly Lazy<NpgsqlConnection> connection;
    private readonly DatabaseSettings databaseSettings;

    public IDbConnection Connection => connection.Value;

    public ApplicationDatabaseContext(DatabaseSettings databaseSettings)
    {
        this.databaseSettings = databaseSettings;
        connection = new Lazy<NpgsqlConnection>(() => GetConnection());
    }

    private NpgsqlConnection GetConnection()
    {
        var result = new NpgsqlConnection(databaseSettings.ConnectionString);
        return result;
    }

    public void Dispose()
    {
        if (connection.IsValueCreated)
        {
            GC.SuppressFinalize(this);
            connection.Value.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (connection.IsValueCreated)
        {
            GC.SuppressFinalize(this);
            await connection.Value.DisposeAsync();
        }
    }
}