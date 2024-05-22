using System.Linq.Expressions;
using Core.Model.Interface;
using Core.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Rds.Interface;

public interface IPlainReadRepository<T, TE, TEO>
	where TE : IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
{
	ValueTask<TEO?> GetByIdAsync(T id, Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes, CancellationToken cancellationToken);

	ValueTask<TEO?> GetAsync(Expression<Func<TE, bool>> filter
	                         , Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
	                         , Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
	                         , Func<TE, TEO>? advancedMapping
	                         , CancellationToken cancellationToken);

	ValueTask<IEnumerable<TEO>> GetAllAsync(Expression<Func<TE, bool>>? filter
	                                        , Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
	                                        , Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
	                                        , Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
	                                        , CancellationToken cancellationToken);

	ValueTask<IEnumerablePage<TEO>> GetPagedAsync(int pageNumber
	                                              , int pageSize
	                                              , Expression<Func<TE, bool>>? filter
	                                              , Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy
	                                              , Func<IQueryable<TE>, IIncludableQueryable<TE, object>>? includes
	                                              , Func<IEnumerable<TE>, IEnumerable<TEO>>? advancedMapping
	                                              , CancellationToken cancellationToken);

	ValueTask<bool> ExistsAsync(Expression<Func<TE, bool>>? filter, CancellationToken cancellationToken);
}