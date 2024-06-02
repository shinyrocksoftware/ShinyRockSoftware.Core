using Confluent.Kafka;
using Core.Attribute.AutoInjection;
using Core.Configuration.Interface;
using Base.Model.Interface;
using Core.Stream.ConnectorModels;
using Core.Stream.Interface;
using Core.Stream.Models;
using Microsoft.Extensions.Logging;

namespace Core.Stream.Consumers;

[ScopedAutoInjection]
internal class StreamConsumer(ILogger<StreamConsumer> logger, IConnectorModelHelper connectorModelHelper) : IStreamConsumer
{
	private readonly ILogger _logger = logger;
	private readonly KafkaConnectorModel _kafkaConnectorModel = connectorModelHelper.GetConnector<KafkaConnectorModel>();

	public async Task ConsumeAsync(string groupId, string topic, Func<IStreamMessage, Task> consumeAction)
	{
		await ConsumeAsync(groupId, topic, async consumeResult =>
		{
			await consumeAction(new StreamMessage(consumeResult));
		});
	}

	public async Task ConsumeAsync(string groupId, string topic, Func<ConsumeResult<Ignore, string>, Task> consumeAction)
	{
		var consumerConfig = GetConsumerConfig(groupId);

		using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
		consumer.Subscribe(topic);

		try
		{
			while (true)
			{
				var result = consumer.Consume(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerTimeoutBySecond));

				if (result == null)
				{
					consumer.Close();
					Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerRebootTimeBySecond));
					break;
				}

				await consumeAction(result);

				consumer.Commit();
				Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerProcessIntervalBySecond));
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);

			consumer.Close();
			Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerRebootTimeBySecond));
		}

		await Task.CompletedTask;
	}

	public void Consume(string groupId, string topic, Action<IStreamMessage> consumeAction)
	{
		Consume(groupId, topic, consumeResult =>
		{
			consumeAction(new StreamMessage(consumeResult));
		});
	}

	public void Consume(string groupId, string topic, Action<ConsumeResult<Ignore, string>> consumeAction)
	{
		var consumerConfig = GetConsumerConfig(groupId);

		using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
		consumer.Subscribe(topic);

		try
		{
			while (true)
			{
				var result = consumer.Consume(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerTimeoutBySecond));

				if (result == null)
				{
					consumer.Close();
					Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerRebootTimeBySecond));
					break;
				}

				consumeAction(result);

				consumer.Commit();
				Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerProcessIntervalBySecond));
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);

			consumer.Close();
			Thread.Sleep(TimeSpan.FromSeconds(_kafkaConnectorModel.ConsumerRebootTimeBySecond));
		}
	}

	private ConsumerConfig GetConsumerConfig(string groupId)
	{
		return new()
		{
			BootstrapServers = _kafkaConnectorModel.BootstrapServers
			, GroupId = groupId
			, EnableAutoCommit = false
			, AutoOffsetReset = AutoOffsetReset.Earliest
			, AllowAutoCreateTopics = true
		};
	}
}