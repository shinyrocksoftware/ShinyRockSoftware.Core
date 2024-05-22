using Core.Model.Interface.Entities;
using Core.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.DbRequests.Partial;

public abstract class BasePartialCommandDbRequest<T, TE, TBE> : BaseCommandDbRequest<T, TE, TBE>
	where TE : IEntity<T>, new()
	where TBE : INotificationEvent, new();