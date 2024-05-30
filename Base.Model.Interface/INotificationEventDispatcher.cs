using Base.Model.Interface.Entities;

namespace Base.Model.Interface;

public interface INotificationEventDispatcher : IAutoInjection
{
	Task DispatchAndClearEventsAsync<T>(IEnumerable<IPlainEntity<T>> entitiesWithEvents);
}