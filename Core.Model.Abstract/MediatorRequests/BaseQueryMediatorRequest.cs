using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;
using Base.ObjectMapper.Extension;

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