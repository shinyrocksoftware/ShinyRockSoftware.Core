using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Rds.Abstract.Repositories;

public abstract class BaseReadRepository<T, TE, TEO, TDC>(ILogger<BaseReadRepository<T, TE, TEO, TDC>> logger, TDC dataContext)
	: BasePlainReadRepository<T, TE, TEO, TDC>(logger, dataContext)
	where TE : class, IPlainEntity<T>
	where TEO : IPlainEntityDto<T>, new()
	where TDC : DbContext
{
}