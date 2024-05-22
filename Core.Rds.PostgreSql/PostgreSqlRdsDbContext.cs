using Core.Rds.Interface;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.PostgreSql;

public class PostgreSqlRdsDbContext : IRdsDbContext
{
    public void AddDbContext(DbContextOptionsBuilder options, string connectionString)
    {
	    options.UseNpgsql(connectionString)
	           .UseSnakeCaseNamingConvention();
    }
}