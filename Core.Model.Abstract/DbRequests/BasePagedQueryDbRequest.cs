using Base.Model.Interface.Entities;

namespace Core.Model.Abstract.DbRequests;

public abstract class BasePagedQueryDbRequest<T, TE> : BaseQueryDbRequest<T, TE>
	where TE : IEntity<T>, new()
{
	public int PageSize { get; set; }
	public int PageNumber { get; set; }
}