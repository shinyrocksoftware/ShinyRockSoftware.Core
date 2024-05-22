using Core.Rds.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.SqlServer;

public class SqlServerRdsDbContext : IRdsDbContext
{
    public void AddDbContext(DbContextOptionsBuilder options, string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

        options.UseSqlServer(connectionStringBuilder.ToString());
    }
}