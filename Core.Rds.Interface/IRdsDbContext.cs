using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Interface;

public interface IRdsDbContext
{
	void AddDbContext(DbContextOptionsBuilder options, string connectionString);
}