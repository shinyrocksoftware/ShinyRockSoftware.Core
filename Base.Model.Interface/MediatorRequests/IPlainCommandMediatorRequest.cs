using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;

namespace Base.Model.Interface.MediatorRequests;

public interface IPlainCommandMediatorRequest<T, TE, TEO, TV> : IMediatorRequest<T, TEO>
	where TV : IPlainCommandDbRequest<T, TE>
	where TE : IPlainEntity<T>
	where TEO : IPlainEntityDto<T>
{
	TV ToDbRequest();
}

public interface IPlainCommandMediatorRequest<T, TE, TV> : IMediatorRequest<T>
	where TV : IPlainCommandDbRequest<T, TE>
	where TE : IPlainEntity<T>
{
	TV ToDbRequest();
}