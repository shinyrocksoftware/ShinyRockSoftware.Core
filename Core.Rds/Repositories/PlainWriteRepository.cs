using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Core.Rds.Abstract.Repositories;
using Core.Rds.DbContexts;
using Core.Rds.Interface;

namespace Core.Rds.Repositories;

public class PlainWriteRepository<T, TE>(INotificationEventDispatcher dispatcher, PlainWriteDbContext dataContext)
	: BasePlainWriteRepository<T, TE, PlainWriteDbContext>(dispatcher, dataContext), IPlainWriteRepository<T, TE>
	where TE : class, IPlainEntity<T>;