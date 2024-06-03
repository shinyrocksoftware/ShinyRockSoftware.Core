using System.Linq.Expressions;
using Base.Extension;
using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Base.ObjectMapper.Extension;
using Core.Rds.Abstract.Extensions;
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

	public async ValueTask<TEO?> GetByIdAsync(
		T id
		, Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
		, CancellationToken cancellationToken
	)
	{
		var query = Query(includes).Where(c => c.Id != null && c.Id.Equals(id));

		PrintQuery(query);

		var entities = await query.ToListAsync(cancellationToken);

		if (entities.IsNotNullNorEmpty())
		{
			var entity = entities.FirstOrDefault();

			if (entity != null)
			{
				return entity.To<TEO>();
			}
		}

		return default;
	}

	public async ValueTask<TEO?> GetAsync(
		Expression<Func<TE, bool>> filter
		, Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
		, Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
		, Func<TE, TEO>? advancedMapping
		, CancellationToken cancellationToken
	)
	{
		Func<IEnumerable<TE>, IEnumerable<TEO>>? getAllAdvancedMapping = default;

		if (advancedMapping != null)
		{
			getAllAdvancedMapping = entities =>
			{
				var entityDtos = new List<TEO>();

				if (entities.IsNotNullNorEmpty())
				{
					var entity = entities.FirstOrDefault();

					if (entity != null)
					{
						entityDtos.Add(advancedMapping(entity));
					}
				}

				return entityDtos;
			};
		}

		return (await GetAllAsync(filter, orderBy, includes, getAllAdvancedMapping, cancellationToken)).FirstOrDefault();
	}

	public async ValueTask<IEnumerable<TEO>> GetAllAsync(
		Expression<Func<TE, bool>>? filter
		, Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
		, Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
		, Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
		, CancellationToken cancellationToken
	)
	{
		var query = Query(includes);

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (orderBy != null)
		{
			query = query.OrderBy<T, TE>(orderBy);
		}

		var entities = await query.ToListAsync(cancellationToken);

		PrintQuery(query);

		return advancedMapping == null
			? entities.Select(c => c.To<TEO>())
			: advancedMapping(entities);
	}

	public async ValueTask<IEnumerablePage<TEO>> GetPagedAsync(
		int pageNumber, int pageSize
		, Expression<Func<TE, bool>>? filter
		, Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
		, Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
		, Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
		, CancellationToken cancellationToken
	)
	{
		var pagingQuery = Query(includes);

		if (filter != null)
		{
			pagingQuery = pagingQuery.Where(filter);
		}

		if (orderBy != null)
		{
			pagingQuery = pagingQuery.OrderBy<T, TE>(orderBy);
		}

		var total = await pagingQuery.CountAsync(cancellationToken);
		var query = pagingQuery.Skip((pageNumber - 1) * pageSize)
		                       .Take(pageSize);
		var entities = await query.ToListAsync(cancellationToken);

		PrintQuery(query);

		var data = advancedMapping == null
			? entities.Select(c => c.To<TEO>())
			: advancedMapping(entities);

		return data.CreatePage(pageNumber, pageSize, total);
	}

	public async ValueTask<bool> ExistsAsync(Expression<Func<TE, bool>> filter, CancellationToken cancellationToken)
	{
		IQueryable<TE> query = DataContext.Set<TE>();

		PrintQuery(query);

		return await query.AnyAsync(filter, cancellationToken);
	}

	#region Internal Methods

	internal IQueryable<TE> Query(Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes)
	{
		IQueryable<TE> query = DataContext.Set<TE>();

		var innerIncludes = includes;

		if (innerIncludes == null && DefaultIncludes != null)
		{
			innerIncludes = DefaultIncludes;
		}

		if (innerIncludes != null)
		{
			query = innerIncludes(query);
		}

		return query;
	}

	protected void PrintQuery(IQueryable<TE> query)
	{
		#if DEBUG

		try
		{
			Logger.LogInformation(query.ToQueryString());
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

		#endif
	}

	#endregion
}