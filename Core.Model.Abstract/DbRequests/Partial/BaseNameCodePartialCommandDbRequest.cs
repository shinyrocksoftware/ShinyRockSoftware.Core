using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Base.Model.Interface.MediatorEvents;

namespace Core.Model.Abstract.DbRequests.Partial;

public abstract class BaseNameCodePartialCommandDbRequest<T, TE, TBE> : BasePartialCommandDbRequest<T, TE, TBE>
                                                                             , INameModel, ICodeModel
	where TE : IEntity<T>, new()
	where TBE : INotificationEvent, new()
{
	public string Name { get; set; }
	public string Code { get; set; }
}