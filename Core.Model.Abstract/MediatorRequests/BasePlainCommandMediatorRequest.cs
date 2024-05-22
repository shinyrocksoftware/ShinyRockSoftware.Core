using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;
using Core.ObjectMapper.Extensions;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePlainCommandMediatorRequest<T, TE, TEO, TV> : BaseMediatorRequest<T, TEO>
	where TV : IPlainCommandDbRequest<T, TE>
	where TE : IPlainEntity<T>
	where TEO : IPlainEntityDto<T>
{
	public BasePlainCommandMediatorRequest()
	{
	}

	public BasePlainCommandMediatorRequest(T id) : base(id)
	{
	}

	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}

public abstract class BasePlainCommandMediatorRequest<T, TE, TV> : BaseMediatorRequest<T>
	where TV : IPlainCommandDbRequest<T, TE>
	where TE : IPlainEntity<T>
{
	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}