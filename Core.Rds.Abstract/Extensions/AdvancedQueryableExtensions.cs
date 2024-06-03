using System.Linq.Expressions;
using Base.Extension;
using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Base.Model.Rds;
using Base.ObjectMapper.Extension;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Abstract.Extensions;

public static class AdvancedQueryableExtensions
{
	public static AdvancedQueryable<T, TE, TEO> Where<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, Expression<Func<TE, bool>> filter)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.Queryable = advancedQueryable.Queryable.Where(filter);

		return advancedQueryable;
	}

	public static AdvancedQueryable<T, TE, TEO> OrderBy<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, Func<IQueryable<TE>, IOrderedQueryable<TE>> orderBy)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.Queryable = orderBy(advancedQueryable.Queryable);

		return advancedQueryable;
	}

	public static async ValueTask<TEO?> SelectByIdAsync<T, TE, TEO>(
		this AdvancedQueryable<T, TE, TEO> advancedQueryable
		, T id
		, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		return await SelectByIdAsync(advancedQueryable, id, null, cancellationToken);
	}

	public static async ValueTask<TEO?> SelectByIdAsync<T, TE, TEO>(
		this AdvancedQueryable<T, TE, TEO> advancedQueryable
		, T id
		, Func<TE, TEO>? advancedMapping
		, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.Where(c => c.Id != null && c.Id.Equals(id));

		advancedQueryable.PrintQuery();

		var entity = await advancedQueryable.Queryable.FirstOrDefaultAsync(cancellationToken);

		return entity == null
			? default
			: advancedMapping == null
				? entity.To<TEO>()
				: advancedMapping(entity);
	}

	public static async ValueTask<TEO?> SelectOneAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		return await SelectOneAsync(advancedQueryable, null, cancellationToken);
	}

	public static async ValueTask<TEO?> SelectOneAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, Func<TE, TEO>? advancedMapping, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.PrintQuery();

		var entity = await advancedQueryable.Queryable.FirstOrDefaultAsync(cancellationToken);

		return entity == null
			? default
			: advancedMapping == null
				? entity.To<TEO>()
				: advancedMapping(entity);
	}

	public static async ValueTask<IEnumerable<TEO>> SelectTopAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, int top, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		return await SelectTopAsync(advancedQueryable, top, null, cancellationToken);
	}

	public static async ValueTask<IEnumerable<TEO>> SelectTopAsync<T, TE, TEO>(
		this AdvancedQueryable<T, TE, TEO> advancedQueryable
		, int top
		, Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
		, CancellationToken cancellationToken
	)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.Queryable = advancedQueryable.Queryable.Take(top);

		advancedQueryable.PrintQuery();

		var entities = await advancedQueryable.Queryable.ToListAsync(cancellationToken);

		return advancedMapping == null
			? entities.Select(c => c.To<TEO>())
			: advancedMapping(entities);
	}

	public static async ValueTask<IEnumerable<TEO>> SelectAllAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		return await SelectAllAsync(advancedQueryable, null, cancellationToken);
	}

	public static async ValueTask<IEnumerable<TEO>> SelectAllAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		advancedQueryable.PrintQuery();

		var entities = await advancedQueryable.Queryable.ToListAsync(cancellationToken);

		return advancedMapping == null
			? entities.Select(c => c.To<TEO>())
			: advancedMapping(entities);
	}

	public static async ValueTask<IEnumerablePage<TEO>> SelectPagedAsync<T, TE, TEO>(this AdvancedQueryable<T, TE, TEO> advancedQueryable, int pageNumber, int pageSize, CancellationToken cancellationToken)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		return await SelectPagedAsync(advancedQueryable, pageNumber, pageSize, null, cancellationToken);
	}

	public static async ValueTask<IEnumerablePage<TEO>> SelectPagedAsync<T, TE, TEO>(
		this AdvancedQueryable<T, TE, TEO> advancedQueryable
		, int pageNumber
		, int pageSize
		, Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
		, CancellationToken cancellationToken
	)
		where TE : class, IPlainEntity<T>
		where TEO : IPlainEntityDto<T>, new()
	{
		var pagingQuery = advancedQueryable.Queryable;

		var total = await pagingQuery.CountAsync(cancellationToken);
		var query = pagingQuery.Skip((pageNumber - 1) * pageSize)
		                       .Take(pageSize);

		advancedQueryable.PrintQuery();

		var entities = await query.ToListAsync(cancellationToken);

		var data = advancedMapping == null
			? entities.Select(c => c.To<TEO>())
			: advancedMapping(entities);

		return data.CreatePage(pageNumber, pageSize, total);
	}
}