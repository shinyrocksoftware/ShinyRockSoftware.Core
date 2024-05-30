using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;
using Base.ObjectMapper.Extension;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePagedQueryMediatorRequest<T, TEO, TV>(int pageNumber, int pageSize)
	: BasePagedMediatorRequest<T, TEO>(pageNumber, pageSize)
	where TV : IPagedQueryDbRequest<T>, new()
	where TEO : IEntityDto<T>, new()
{
	public virtual TV ToDbRequest()
	{
		return this.To<TV>();
	}
}