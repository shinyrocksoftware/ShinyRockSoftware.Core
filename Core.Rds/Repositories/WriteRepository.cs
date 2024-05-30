using Core.Helper.Interface;
using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Core.Rds.Abstract.Repositories;
using Core.Rds.DbContexts;
using Core.Rds.Interface;

namespace Core.Rds.Repositories;

public class WriteRepository<T, TE>(INotificationEventDispatcher dispatcher, WriteDbContext dataContext, IDateTimeHelper dateTimeHelper)
	: BaseWriteRepository<T, TE, WriteDbContext>(dispatcher, dataContext, dateTimeHelper), IWriteRepository<T, TE>
	where TE : class, IEntity<T>;