using Base.Model.Interface;
using Base.Model.Interface.Entities;
using MediatR;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePagedMediatorRequest<T, TEO>(int pageNumber, int pageSize) : IRequest<IEnumerablePage<TEO>>
	where TEO : IEntityDto<T>, new()
{
	public int PageNumber { get; set; } = pageNumber;
	public int PageSize { get; set; } = pageSize;
}