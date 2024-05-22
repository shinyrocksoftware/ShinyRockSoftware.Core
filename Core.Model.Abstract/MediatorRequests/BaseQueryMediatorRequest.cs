using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;
using Core.ObjectMapper.Extensions;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BaseQueryMediatorRequest<T, TEO, TV>(T id)
	: BaseMediatorRequest<T, TEO>(id)
	where TEO : IPlainEntityDto<T>, new()
	where TV : IQueryDbRequest<T>, new()
{
	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}