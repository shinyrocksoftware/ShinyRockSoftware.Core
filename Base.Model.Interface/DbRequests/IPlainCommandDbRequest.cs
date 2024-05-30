using Base.Model.Interface.Entities;

namespace Base.Model.Interface.DbRequests;

public interface IPlainCommandDbRequest<T, TE> : IDbRequest
	where TE : IPlainEntity<T>
{
	public T? Id { get; set; }
	public TE ToEntity();
}