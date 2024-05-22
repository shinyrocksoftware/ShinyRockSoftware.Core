using Core.Rds.Abstract.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.DbContexts;

public class PlainWriteDbContext(DbContextOptions<PlainWriteDbContext> options)
	: BasePlainCoreDbContext<PlainWriteDbContext>(options);