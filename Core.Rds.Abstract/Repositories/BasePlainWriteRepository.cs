using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Base.Extension;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Abstract.Repositories;

public abstract class BasePlainWriteRepository<T, TE, TDC>(INotificationEventDispatcher dispatcher, TDC dataContext)
	where TE : class, IPlainEntity<T>
	where TDC : DbContext
{
	public async ValueTask BulkInsertAsync(IEnumerable<TE> entities, CancellationToken cancellationToken)
	{
		dataContext.ChangeTracker.AutoDetectChangesEnabled = false;

		var valueTasks = entities.Select(entity => dataContext.Set<TE>().AddAsync(entity, cancellationToken)).Cast<ValueTask<TE>>();

		await valueTasks.WhenAll();

		dataContext.ChangeTracker.DetectChanges();

		await dataContext.SaveChangesAsync(cancellationToken);

		dataContext.ChangeTracker.AutoDetectChangesEnabled = true;
	}

	public async ValueTask<TE> AddAsync(TE entity, CancellationToken cancellationToken)
	{
		await dataContext.Set<TE>().AddAsync(entity, cancellationToken);

		return entity;
	}

	public async ValueTask<TE> UpdateByIdAsync(object id, TE entity, CancellationToken cancellationToken)
	{
		return await ExecuteByIdAsync(id, inDatabaseEntity =>
		{
			PopulateOriginalData(inDatabaseEntity, entity);
			GetChangedData(inDatabaseEntity, entity);

			inDatabaseEntity.ClearNotificationEvents();
			inDatabaseEntity.AddNotificationEvents(entity.NotificationEvents);
		}, cancellationToken);
	}

	public async ValueTask DeleteByIdAsync(object id, TE entity, CancellationToken cancellationToken)
	{
		await ExecuteByIdAsync(id, inDatabaseEntity =>
		{
			dataContext.Set<TE>().Remove(inDatabaseEntity);

			PopulateOriginalData(inDatabaseEntity, entity);

			inDatabaseEntity.ClearNotificationEvents();
			inDatabaseEntity.AddNotificationEvents(entity.NotificationEvents);
		}, cancellationToken);
	}

	public async ValueTask<int> SaveAsync(CancellationToken cancellationToken)
	{
		var entitiesWithEvents = dataContext.ChangeTracker.Entries<TE>()
		                                    .Select(e => e.Entity)
		                                    .Where(e => e.NotificationEvents.Count != 0)
		                                    .ToArray();

		var result = await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		if (dispatcher != null && result > 0)
		{
			await dispatcher.DispatchAndClearEventsAsync(entitiesWithEvents);
		}

		return result;
	}

	#region Internal Methods

	internal async Task<TE> ExecuteByIdAsync(object id, Action<TE> entityExistAction, CancellationToken cancellationToken)
	{
		var entity = await dataContext.Set<TE>().FindAsync([id], cancellationToken);

		if (entity != null)
		{
			entityExistAction(entity);
			return entity;
		}

		throw new NullReferenceException($"Entity {typeof(TE).Name} with {id} is not found");
	}

	internal void PopulateOriginalData(object originalObj, object updateObj)
	{
		foreach (var property in updateObj.GetType().GetProperties())
		{
			var updatedValue = property.GetValue(updateObj, null);
			if (updatedValue == null || (property.PropertyType == typeof(DateTime) && (DateTime)updatedValue == DateTime.MinValue))
			{
				property.SetValue(updateObj, originalObj.GetType().GetProperty(property.Name)?.GetValue(originalObj, null));
			}
		}
	}

	internal void GetChangedData(TE inDatabaseEntity, TE entity)
	{
		dataContext.Entry(inDatabaseEntity).CurrentValues.SetValues(entity);
	}

	#endregion
}