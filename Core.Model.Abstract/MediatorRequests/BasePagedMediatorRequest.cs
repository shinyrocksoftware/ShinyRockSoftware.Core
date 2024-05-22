using Core.Model.Interface;
using Core.Model.Interface.Entities;
using MediatR;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePagedMediatorRequest<T, TEO> : IRequest<IEnumerablePage<TEO>>
	where TEO : IEntityDto<T>, new()
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

	public BasePagedMediatorRequest(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}