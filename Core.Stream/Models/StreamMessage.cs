using Confluent.Kafka;
using Core.Model.Interface;

namespace Core.Stream.Models;

public class StreamMessage : IStreamMessage
{
	public string Topic { get; set; }
	public string Value { get; set; }
	public DateTime Timestamp { get; set; }

	public StreamMessage(ConsumeResult<Ignore, string> consumeResult)
	{
		Topic = consumeResult.Topic;
		Value = consumeResult.Message.Value;
		Timestamp = consumeResult.Message.Timestamp.UtcDateTime;
	}
}
