using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Base.Model.Rds;

public class AdvancedQueryable<T, TE, TEO>(ILogger logger, IQueryable<TE> queryable)
	where TE : class, IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
{
	public readonly ILogger Logger = logger;
	public IQueryable<TE> Queryable = queryable;

	public void PrintQuery()
	{
		#if DEBUG

		try
		{
			Logger.LogInformation(Queryable.ToQueryString());
		}
		catch (Exception e)
		{
			Logger.LogError(e, e.Message);
		}

		#endif
	}
}