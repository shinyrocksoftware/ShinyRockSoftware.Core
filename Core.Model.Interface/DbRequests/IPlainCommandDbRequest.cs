using Core.Model.Interface.Entities;

namespace Core.Model.Interface.DbRequests;

public interface IPlainCommandDbRequest<T, TE> : IDbRequest
	where TE : IPlainEntity<T>
{
	public T? Id { get; set; }
	public TE ToEntity();
}