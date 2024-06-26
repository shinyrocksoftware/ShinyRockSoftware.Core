﻿using Base.Constant;
using Base.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.NotificationEvents;

public abstract class BaseUpdatedNotificationEvent : BaseNotificationEvent, INotificationEvent
{
	public override string ActionType { get; set; } = NotificationActionTypeConstants.UPDATED;
}