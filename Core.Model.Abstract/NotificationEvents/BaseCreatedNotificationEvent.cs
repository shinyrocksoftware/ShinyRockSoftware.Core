using Core.Constant;
using Core.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.NotificationEvents;

public abstract class BaseCreatedNotificationEvent : BaseNotificationEvent, INotificationEvent
{
	public override string ActionType { get; set; } = NotificationActionTypeConstants.CREATED;
}