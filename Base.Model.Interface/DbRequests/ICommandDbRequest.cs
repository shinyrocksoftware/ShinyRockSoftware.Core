using Base.Model.Interface.Entities;

namespace Base.Model.Interface.DbRequests;

public interface ICommandDbRequest<T, TE> : IDbRequest
	where TE : IEntity<T>
{
	public T? Id { get; set; }
	public TE ToEntity();
}