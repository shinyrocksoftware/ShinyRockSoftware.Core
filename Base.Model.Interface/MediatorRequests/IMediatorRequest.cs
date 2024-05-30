using Base.Model.Interface.Entities;
using MediatR;

namespace Base.Model.Interface.MediatorRequests;

public interface IMediatorRequest<T, TEO> : IRequest<TEO>
	where TEO : IPlainEntityDto<T>
{
	T Id { get; set; }
}

public interface IMediatorRequest<T> : IRequest<bool>
{
	T Id { get; set; }
}