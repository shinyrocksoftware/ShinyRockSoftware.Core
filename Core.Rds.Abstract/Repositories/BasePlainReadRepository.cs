using System.Linq.Expressions;
using Base.Model.Interface.Entities;
using Base.Model.Rds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Core.Rds.Abstract.Repositories;

public abstract class BasePlainReadRepository<T, TE, TEO, TDC>(ILogger<BasePlainReadRepository<T, TE, TEO, TDC>> logger, TDC dataContext)
	where TE : class, IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
	where TDC : DbContext
{
	protected abstract Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? DefaultIncludes { get; set; }

	protected readonly ILogger Logger = logger;
	protected readonly TDC DataContext = dataContext;

	public AdvancedQueryable<T, TE, TEO> Query(Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes = null)
	{
		IQueryable<TE> queryable = DataContext.Set<TE>();

		var innerIncludes = includes;

		if (innerIncludes == null && DefaultIncludes != null)
		{
			innerIncludes = DefaultIncludes;
		}

		if (innerIncludes != null)
		{
			queryable = innerIncludes(queryable);
		}

		return new(logger, queryable);
	}

	public async ValueTask<bool> ExistsAsync(Expression<Func<TE, bool>> filter, CancellationToken cancellationToken)
	{
		IQueryable<TE> query = DataContext.Set<TE>();

		PrintQuery(query);

		return await query.AnyAsync(filter, cancellationToken);
	}

	#region Internal Methods

	protected void PrintQuery(IQueryable<TE> query)
	{
		#if DEBUG

		try
		{
			Logger.LogInformation(query.ToQueryString());
		}
		catch (Exception e)
		{
			Logger.LogError(e, e.Message);
		}

		#endif
	}

	#endregion
}