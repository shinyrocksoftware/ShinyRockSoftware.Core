using Core.Model.Interface.DbRequests;
using Core.Model.Interface.Entities;

namespace Core.Model.Interface.MediatorRequests;

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