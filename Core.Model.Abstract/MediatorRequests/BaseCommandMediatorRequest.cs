using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;
using Core.ObjectMapper.Extensions;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BaseCommandMediatorRequest<T, TE, TEO, TV> : BaseMediatorRequest<T, TEO>
	where TV : ICommandDbRequest<T, TE>
	where TE : IEntity<T>
	where TEO : IEntityDto<T>
{
	public BaseCommandMediatorRequest()
	{
	}

	public BaseCommandMediatorRequest(T id) : base(id)
	{
	}

	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}

public abstract class BaseCommandMediatorRequest<T, TE, TV> : BaseMediatorRequest<T>
	where TV : ICommandDbRequest<T, TE>
	where TE : IEntity<T>
{
	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}