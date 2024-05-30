using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;
using Base.ObjectMapper.Extension;

namespace Core.Model.Abstract.DbRequests;

public abstract class BaseQueryDbRequest<T, TE> : BaseDbRequest, IQueryDbRequest<T>
	where TE : IEntity<T>
{
	public T? Id { get; set; }

	public TE ToEntity()
	{
		return this.To<TE>();
	}
}