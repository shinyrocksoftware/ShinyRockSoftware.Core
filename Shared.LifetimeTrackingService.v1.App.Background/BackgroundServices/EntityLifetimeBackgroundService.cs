using Core.BackgroundService.Interface;
using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;
using Core.Extension;
using Core.Stream.Abstract.BackgroundServices;
using Core.Stream.ConnectorModels;
using Microsoft.Extensions.DependencyInjection;
using Shared.LifetimeTrackingService.v1.App.Background.Stream.StreamJobs.Interfaces;

namespace Shared.LifetimeTrackingService.v1.App.Background.BackgroundServices;

public class EntityLifetimeBackgroundService : BaseStreamBackgroundService, IPeriodicBackgroundService
{
	private readonly ServiceConnectorModel _serviceConnectorModel;

	public override string ServiceName => _serviceConnectorModel.ClientServiceNamePattern.ApplyFormat("changed");
	public override TimeSpan Period { get; }

	public EntityLifetimeBackgroundService(IServiceScopeFactory serviceScopeFactory, IEntityLifetimeChangedStreamJob streamJob)
		: base(serviceScopeFactory, streamJob)
	{
		using var asyncScope = ServiceScopeFactory.CreateScope();

		var connectorModelHelper = asyncScope.ServiceProvider.GetRequiredService<IConnectorModelHelper>();

		_serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();
		var kafkaConnectorModel = connectorModelHelper.GetConnector<KafkaConnectorModel>();

		Period = TimeSpan.FromSeconds(kafkaConnectorModel.ConsumerRebootTimeBySecond);
	}
}