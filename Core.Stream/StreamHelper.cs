using Core.Attribute.AutoInjection;
using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;
using Core.Constant;
using Core.Extension;
using Core.Stream.Interface;

namespace Core.Stream;

[SingletonAutoInjection]
public class StreamHelper : IStreamHelper
{
	private readonly ServiceConnectorModel _serviceConnectorModel;

	public StreamHelper(IConnectorModelHelper connectorModelHelper)
	{
		_serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();
	}

	public string LifetimeTrackingTopic => SharedAppConstants.LIFETIME_TRACKING_TOPIC_PATTERN.ApplyFormat(_serviceConnectorModel.ClientName);
	public string App => _serviceConnectorModel.Code;
	public string Version => _serviceConnectorModel.Version;
}