using Core.Configuration.Interface;
using Core.Logger.Interface;
using Serilog;

namespace Core.Logger.OpenSearch;

public class OpenSearchSinkRegistration : ISinkRegistration
{
	public void Register(ILoggerConnectorModel loggerConnectorModel, LoggerConfiguration loggerConfiguration)
	{
		loggerConfiguration
			.WriteTo.OpenSearch(new(loggerConnectorModel.OpenSearchHostList.Select(c => new Uri(c)))
			{
				FormatStackTraceAsArray = true
			});
	}
}