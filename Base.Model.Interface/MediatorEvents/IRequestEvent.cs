using MediatR;

namespace Base.Model.Interface.MediatorEvents;

public interface IRequestEvent<T> : IRequest<T>;