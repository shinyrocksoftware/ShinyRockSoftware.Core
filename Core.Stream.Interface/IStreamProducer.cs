using Base.Model.Interface;
using Base.Model.Interface.MediatorEvents;

namespace Core.Stream.Interface;

public interface IStreamProducer : IAutoInjection
{
	void Produce(INotificationEvent notificationEvent, Action? postAction);
	Task ProduceAsync(INotificationEvent notificationEvent, Action? postAction, CancellationToken cancellationToken);
}