using Base.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.NotificationEvents;

public abstract class BaseNotificationEvent : INotificationEvent
{
	public string App { get; set; }
	public string Version { get; set; }
	public object Entity { get; set; }
	public DateTime DateOccurred { get; set; } = DateTime.UtcNow;
	public abstract string TopicName { get; set; }
	public abstract string ActionType { get; set; }
}