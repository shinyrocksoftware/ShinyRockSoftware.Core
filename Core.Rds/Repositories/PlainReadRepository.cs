using Core.Model.Interface.Entities;
using Core.Rds.Abstract.Repositories;
using Core.Rds.DbContexts;
using Core.Rds.Interface;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Core.Rds.Repositories;

public class PlainReadRepository<T, TE, TEO>(ILogger<PlainReadRepository<T, TE, TEO>> logger, PlainReadDbContext dataContext)
	: BaseReadRepository<T, TE, TEO, PlainReadDbContext>(logger, dataContext), IPlainReadRepository<T, TE, TEO>
	where TE : class, IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
{
	protected override Func<IQueryable<TE>, IIncludableQueryable<TE, object>> DefaultIncludes { get; set; }
}