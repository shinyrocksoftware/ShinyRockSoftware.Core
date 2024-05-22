using Core.Model.Interface.Entities;

namespace Core.Model.Interface.DbRequests;

public interface ICommandDbRequest<T, TE> : IDbRequest
	where TE : IEntity<T>
{
	public T? Id { get; set; }
	public TE ToEntity();
}