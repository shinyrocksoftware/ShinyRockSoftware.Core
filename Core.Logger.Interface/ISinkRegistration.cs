using Base.Model.Interface;
using Core.Configuration.Interface;
using Serilog;

namespace Core.Logger.Interface;

public interface ISinkRegistration : IAutoInjection
{
	void Register(ILoggerConnectorModel loggerConnectorModel, LoggerConfiguration loggerConfiguration);
}