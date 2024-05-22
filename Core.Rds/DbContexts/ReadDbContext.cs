using Core.Rds.Abstract.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.DbContexts;

public class ReadDbContext(DbContextOptions<ReadDbContext> options)
	: BaseCoreDbContext<ReadDbContext>(options);