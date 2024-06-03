using System.Linq.Expressions;
using Base.Model.Interface.Entities;
using Base.Model.Rds;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Rds.Interface;

public interface IPlainReadRepository<T, TE, TEO>
	where TE : class, IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
{
	AdvancedQueryable<T, TE, TEO> Query(Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes = null);
	ValueTask<bool> ExistsAsync(Expression<Func<TE, bool>> filter, CancellationToken cancellationToken);
}