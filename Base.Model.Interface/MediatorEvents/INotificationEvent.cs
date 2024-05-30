using MediatR;

namespace Base.Model.Interface.MediatorEvents;

public interface INotificationEvent : INotification
{
	public string App { get; set; }
	public string Version { get; set; }
	object Entity { get; set; }
	DateTime DateOccurred { get; set; }
	string TopicName { get; set; }
	string ActionType { get; set; }
}