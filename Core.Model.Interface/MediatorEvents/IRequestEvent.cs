using MediatR;

namespace Core.Model.Interface.MediatorEvents;

public interface IRequestEvent<T> : IRequest<T>;