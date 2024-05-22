using Core.Rds.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Core.Rds.MySql;

public class MySqlRdsDbContext : IRdsDbContext
{
    public void AddDbContext(DbContextOptionsBuilder options, string connectionString)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
        {
            OldGuids = true
        };

        options.UseMySql(connectionStringBuilder.ConnectionString, ServerVersion.AutoDetect(connectionStringBuilder.ConnectionString))
               .UseSnakeCaseNamingConvention();
    }
}