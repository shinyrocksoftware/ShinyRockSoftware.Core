using Core.Rds.Interface;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Sqlite;

public class SqliteRdsDbContext : IRdsDbContext
{
    public void AddDbContext(DbContextOptionsBuilder options, string connectionString)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);

        options.UseSqlite(connectionStringBuilder.ToString());
    }
}