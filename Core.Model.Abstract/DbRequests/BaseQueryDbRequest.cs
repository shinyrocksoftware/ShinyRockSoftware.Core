using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;
using Core.ObjectMapper.Extensions;

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