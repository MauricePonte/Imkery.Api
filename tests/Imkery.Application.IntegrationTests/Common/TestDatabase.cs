using Imkery.Infrastructure.Common.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Imkery.Application.IntegrationTests.Common;

public class SqliteTestDatabase : IDisposable
{
    public SqliteConnection Connection { get; }

    public static SqliteTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqliteTestDatabase("DataSource=:memory:");
        testDatabase.Initialize();
        return testDatabase;
    }

    private SqliteTestDatabase(string connectionString)
    {
        Connection = new SqliteConnection(connectionString);
    }

    private void Initialize()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(Connection)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Dispose();
        Initialize();
    }

    public void Dispose()
    {
        Connection.Close();
    }
}