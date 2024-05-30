using Base.Model.Interface.ApiRequests;

namespace Core.Model.Abstract.ApiRequests;

public abstract class BaseCommandApiRequest<T> : BaseApiRequest, ICommandApiRequest<T>
{
}