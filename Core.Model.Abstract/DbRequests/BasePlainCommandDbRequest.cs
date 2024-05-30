using Core.Model.Interface.Entities;
using Core.Model.Interface.MediatorEvents;
using Core.ObjectMapper.Extensions;

namespace Core.Model.Abstract.DbRequests;

public abstract class BasePlainCommandDbRequest<T, TE> : BaseDbRequest
	where TE : IPlainEntity<T>
{
	public T? Id { get; set; }

	public virtual TE ToEntity()
	{
		var entity = this.To<TE>();

		return entity;
	}
}

public abstract class BasePlainCommandDbRequest<T, TE, TBE> : BaseDbRequest
	where TE : IPlainEntity<T>
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