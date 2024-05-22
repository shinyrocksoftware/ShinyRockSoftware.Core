using Confluent.Kafka;
using Core.Model.Interface;

namespace Core.Stream.Interface;

public interface IStreamConsumer : IAutoInjection
{
	Task ConsumeAsync(string groupId, string topic, Func<IStreamMessage, Task> consumeAction);
	Task ConsumeAsync(string groupId, string topic, Func<ConsumeResult<Ignore, string>, Task> consumeAction);

	void Consume(string groupId, string topic, Action<IStreamMessage> consumeAction);
	void Consume(string groupId, string topic, Action<ConsumeResult<Ignore, string>> consumeAction);
}