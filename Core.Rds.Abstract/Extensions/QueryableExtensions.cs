using Base.Model.Interface.Entities;

namespace Core.Rds.Abstract.Extensions;

internal static class QueryableExtensions
{
	internal static IQueryable<TE> OrderBy<T, TE>(this IQueryable<TE> queryable, Func<IQueryable<TE>, IOrderedQueryable<TE>> orderBy)
		where TE : IPlainEntity<T>
	{
		if (orderBy != null)
		{
			queryable = orderBy(queryable);
		}

		return queryable;
	}
}