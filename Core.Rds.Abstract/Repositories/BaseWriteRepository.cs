using Core.Helper.Interface;
using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Abstract.Repositories;

public abstract class BaseWriteRepository<T, TE, TDC>(INotificationEventDispatcher dispatcher, TDC dataContext, IDateTimeHelper dateTimeHelper)
	: BasePlainWriteRepository<T, TE, TDC>(dispatcher, dataContext)
	where TE : class, IEntity<T>
	where TDC : DbContext
{
	public new async ValueTask<TE> AddAsync(TE entity, CancellationToken cancellationToken)
	{
		entity.IsActive = true;

		await dataContext.Set<TE>().AddAsync(entity, cancellationToken);

		return entity;
	}

	public new async ValueTask<TE> UpdateByIdAsync(T id, TE entity, CancellationToken cancellationToken)
	{
		return await ExecuteByIdAsync(id, inDatabaseEntity =>
		{
			inDatabaseEntity.LastModifiedAt = dateTimeHelper.GetUtcNow();
			inDatabaseEntity.LastModifiedBy = Guid.Empty;

			PopulateOriginalData(inDatabaseEntity, entity);
			GetChangedData(inDatabaseEntity, entity);

			inDatabaseEntity.ClearNotificationEvents();
			inDatabaseEntity.AddNotificationEvents(entity.NotificationEvents);
		}, cancellationToken);
	}

	public new async ValueTask DeleteByIdAsync(T id, TE entity, CancellationToken cancellationToken)
	{
		await ExecuteByIdAsync(id, inDatabaseEntity =>
		{
			inDatabaseEntity.IsActive = false;
			inDatabaseEntity.DeletedAt = dateTimeHelper.GetUtcNow();
			inDatabaseEntity.DeletedBy = Guid.Empty;

			PopulateOriginalData(inDatabaseEntity, entity);

			inDatabaseEntity.ClearNotificationEvents();
			inDatabaseEntity.AddNotificationEvents(entity.NotificationEvents);
		}, cancellationToken);
	}
}