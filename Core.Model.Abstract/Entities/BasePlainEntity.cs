using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Base.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.Entities;

public abstract class BasePlainEntity<T>
{
	public T Id { get; set; }

	[ConcurrencyCheck]
	[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public byte[] RowVersion { get; set; }

	[NotMapped]
	[JsonIgnore]
	public ICollection<INotificationEvent> NotificationEvents { get; } = new List<INotificationEvent>();

	public void AddNotificationEvent(INotificationEvent notificationEvent)
	{
		notificationEvent.Entity = this;
		notificationEvent.DateOccurred = DateTime.UtcNow;

		NotificationEvents.Add(notificationEvent);
	}

	public void AddNotificationEvents(IEnumerable<INotificationEvent> notificationEvents)
	{
		foreach (var notificationEvent in notificationEvents)
		{
			NotificationEvents.Add(notificationEvent);
		}
	}

	public void ClearNotificationEvents() => NotificationEvents.Clear();
}