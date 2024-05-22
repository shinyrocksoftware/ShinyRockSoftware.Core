using Core.Rds.Abstract.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.DbContexts;

public class PlainReadDbContext(DbContextOptions<PlainReadDbContext> options)
	: BasePlainCoreDbContext<PlainReadDbContext>(options);