using Core.Configuration.Interface;
using Serilog;

namespace Core.Logger.Interface;

public interface ISinkRegistration
{
	void Register(ILoggerConnectorModel loggerConnectorModel, LoggerConfiguration loggerConfiguration);
}