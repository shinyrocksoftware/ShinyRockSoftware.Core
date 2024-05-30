using Base.Extension;
using Core.Attribute.AutoInjection;
using Confluent.Kafka;
using Core.Configuration.Interface;
using Base.Model.Interface.MediatorEvents;
using Core.Stream.Interface;
using Core.Stream.ConnectorModels;
using Microsoft.Extensions.Logging;

namespace Core.Stream.Producers;

[ScopedAutoInjection]
internal class StreamProducer(ILogger<StreamProducer> logger, IConnectorModelHelper connectorModelHelper)
	: IStreamProducer
{
	private readonly ILogger _logger = logger;
	private readonly KafkaConnectorModel _kafkaConnectorModel = connectorModelHelper.GetConnector<KafkaConnectorModel>();

	public void Produce(INotificationEvent notificationEvent, Action? postAction)
	{
		var config = GetConsumerConfig();

		try
		{
			using var producer = new ProducerBuilder<Null, string>(config).Build();

			producer.Produce(notificationEvent.TopicName, new()
			{
				Value = notificationEvent.Serialize()
			});

			producer.Flush(TimeSpan.FromSeconds(10));

			postAction?.Invoke();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
		}
	}

	public async Task ProduceAsync(INotificationEvent notificationEvent, Action? postAction, CancellationToken cancellationToken)
	{
		var config = GetConsumerConfig();

		try
		{
			using var producer = new ProducerBuilder<Null, string>(config).Build();

			await producer.ProduceAsync(notificationEvent.TopicName, new()
			{
				Value = notificationEvent.Serialize()
			}, cancellationToken);

			postAction?.Invoke();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
		}
	}

	private ConsumerConfig GetConsumerConfig()
	{
		return new()
		{
			BootstrapServers = _kafkaConnectorModel.BootstrapServers
		};
	}
}