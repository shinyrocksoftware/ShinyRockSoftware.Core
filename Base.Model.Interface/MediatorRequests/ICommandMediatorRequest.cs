using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;

namespace Base.Model.Interface.MediatorRequests;

public interface ICommandMediatorRequest<T, TE, TEO, TV> : IMediatorRequest<T, TEO>
	where TV : ICommandDbRequest<T, TE>
	where TE : IEntity<T>
	where TEO : IEntityDto<T>
{
	TV ToDbRequest();
}

public interface ICommandMediatorRequest<T, TE, TV> : IMediatorRequest<T>
	where TV : ICommandDbRequest<T, TE>
	where TE : IEntity<T>
{
	TV ToDbRequest();
}