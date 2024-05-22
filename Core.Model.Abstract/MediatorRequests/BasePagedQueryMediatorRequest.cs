using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;
using Core.ObjectMapper.Extensions;

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