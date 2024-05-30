using Base.Model.Interface.ApiRequests;

namespace Core.Model.Abstract.ApiRequests;

public abstract class BaseApiRequest : IApiRequest
{
	public virtual string? RequestId { get; set; }
}