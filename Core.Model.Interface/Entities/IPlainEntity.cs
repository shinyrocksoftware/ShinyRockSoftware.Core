using Core.Model.Interface.MediatorEvents;

namespace Core.Model.Interface.Entities;

public interface IPlainEntity<T>
{
	T Id { get; set; }
	byte[] RowVersion { get; set; }

	ICollection<INotificationEvent> NotificationEvents { get; }
	void AddNotificationEvent(INotificationEvent notificationEvent);
	void AddNotificationEvents(IEnumerable<INotificationEvent> notificationEvents);
	void ClearNotificationEvents();
}