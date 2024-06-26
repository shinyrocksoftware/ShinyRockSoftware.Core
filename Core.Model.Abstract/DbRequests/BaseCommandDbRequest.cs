using Base.Model.Interface.Entities;
using Base.Model.Interface.MediatorEvents;
using Base.ObjectMapper.Extension;

namespace Core.Model.Abstract.DbRequests;

public abstract class BaseCommandDbRequest<T, TE> : BaseDbRequest
	where TE : IEntity<T>
{
	public T? Id { get; set; }

	public virtual TE ToEntity() => this.To<TE>();
}

public abstract class BaseCommandDbRequest<T, TE, TBE> : BaseDbRequest
	where TE : IEntity<T>
	where TBE : INotificationEvent, new()
{
	public T? Id { get; set; }

	public virtual TE ToEntity()
	{
		var entity = this.To<TE>();

		entity.AddNotificationEvent(new TBE());

		return entity;
	}
}