using Core.Model.Interface.Entities;

namespace Core.Model.Interface;

public interface INotificationEventDispatcher : IAutoInjection
{
	Task DispatchAndClearEventsAsync<T>(IEnumerable<IPlainEntity<T>> entitiesWithEvents);
}