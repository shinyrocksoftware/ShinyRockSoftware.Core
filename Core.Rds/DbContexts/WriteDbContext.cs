using Core.Rds.Abstract.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.DbContexts;

public class WriteDbContext(DbContextOptions<WriteDbContext> options)
	: BaseCoreDbContext<WriteDbContext>(options);