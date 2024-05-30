using Base.Model.Interface.Entities;
using Base.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BaseMediatorRequest<T, TEO> : IRequestEvent<TEO>
	where TEO : IPlainEntityDto<T>
{
	public T Id { get; set; }

	public BaseMediatorRequest()
	{
	}

	public BaseMediatorRequest(T id)
	{
		Id = id;
	}
}

public abstract class BaseMediatorRequest<T> : IRequestEvent<bool>
{
	public T Id { get; set; }
}