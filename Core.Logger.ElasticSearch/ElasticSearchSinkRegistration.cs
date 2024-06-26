using Core.Attribute.AutoInjection;
using Core.Configuration.Interface;
using Core.Logger.Interface;
using Serilog;

namespace Core.Logger.ElasticSearch;

[SingletonAutoInjection]
public class ElasticSearchSinkRegistration : ISinkRegistration
{
	public void Register(ILoggerConnectorModel loggerConnectorModel, LoggerConfiguration loggerConfiguration)
	{
		loggerConfiguration
			.WriteTo.Elasticsearch(new(loggerConnectorModel.ElasticSearchHostList.Select(c => new Uri(c)))
			{
				FormatStackTraceAsArray = true
			});
	}
}