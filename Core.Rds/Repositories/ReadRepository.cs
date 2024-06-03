using Base.Model.Interface.Entities;
using Core.Rds.Abstract.Repositories;
using Core.Rds.DbContexts;
using Core.Rds.Interface;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Core.Rds.Repositories;

public class ReadRepository<T, TE, TEO>(ILogger<ReadRepository<T, TE, TEO>> logger, ReadDbContext dataContext)
	: BaseReadRepository<T, TE, TEO, ReadDbContext>(logger, dataContext), IReadRepository<T, TE, TEO>
	where TE : class, IEntity<T>
	where TEO : IEntityDto<T>, new()
{
	protected override Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? DefaultIncludes { get; set; }
}