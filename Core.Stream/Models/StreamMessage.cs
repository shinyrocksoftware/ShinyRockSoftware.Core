using Confluent.Kafka;
using Base.Model.Interface;

namespace Core.Stream.Models;

public class StreamMessage(ConsumeResult<Ignore, string> consumeResult) : IStreamMessage
{
	public string Topic { get; set; } = consumeResult.Topic;
	public string Value { get; set; } = consumeResult.Message.Value;
	public DateTime Timestamp { get; set; } = consumeResult.Message.Timestamp.UtcDateTime;
}