using Core.Model.Interface.Entities;
using Core.Model.Interface.MediatorEvents;
using Core.ObjectMapper.Extensions;

namespace Core.Model.Abstract.DbRequests;

public abstract class BaseCommandDbRequest<T, TE> : BaseDbRequest
	where TE : IEntity<T>
{
	public T? Id { get; set; }

	public virtual TE ToEntity()
	{
		var entity = this.To<TE>();

		return entity;
	}
}

public abstract class BaseCommandDbRequest<T, TE, TBE> : BaseDbRequest
	where TE : IEntity<T>
	where TBE : INotificationEvent, new()
{
	public T? Id { get; set; }

	public virtual TE ToEntity()
	{
		var entity = this.To<TE>();

		entity?.AddNotificationEvent(new TBE());

		return entity;
	}
}