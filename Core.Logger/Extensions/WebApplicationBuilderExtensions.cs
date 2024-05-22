using Core.Extension;
using Core.Configuration.Interface;
using Core.Configuration.ConnectorModels;
using Core.Constant;
using Core.Helper;
using Core.Logger.ConnectorModels;
using Core.Logger.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Enrichers.Sensitive;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Formatting.OpenSearch;

namespace Core.Logger.Extensions;

public static class WebApplicationBuilderExtensions
{
	private static bool _isInitialized;

	public static WebApplicationBuilder AddLogger(
		this WebApplicationBuilder builder
		, Action<ServiceConnectorModel, LoggerConnectorModel, LoggerConfiguration>? moreConfigurationAction = null
		, ISinkRegistration? advancedSinkRegistration = default
	)
	{
		var configuration = builder.Configuration;
		var services = builder.Services.AddDefaultLogger(configuration);

		builder.Services.AddHttpLogging(logging =>
		{
			logging.LoggingFields = HttpLoggingFields.All;
			logging.CombineLogs = true;
		});

		builder.Host.UseSerilog((_, loggerConfiguration) =>
		{
			if (!_isInitialized)
			{
				var serviceProvider = services.BuildServiceProvider();
				var connectorModelHelper = serviceProvider.GetRequiredService<IConnectorModelHelper>();
				var serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();
				var loggerConnectorModel = connectorModelHelper.GetConnector<LoggerConnectorModel>();

				loggerConfiguration
					.Enrich.WithExceptionDetails(new DestructuringOptionsBuilder())
					.Enrich.WithMemoryUsage()
					.Enrich.WithProperty(LoggerConstants.PROPERTY_SERVICE, serviceConnectorModel.Code)
					.Enrich.WithProperty(LoggerConstants.PROPERTY_VERSION, serviceConnectorModel.Version)
					.Enrich.WithCorrelationId(headerName: "correlation-id", addValueIfHeaderAbsence: true)
					.Enrich.WithClientIp()
					.Enrich.WithMachineName()
					.Enrich.WithEnvironmentName()
					.Enrich.WithSensitiveDataMasking(new SensitiveDataEnricherOptions(MaskingMode.Globally))
					.Enrich.WithAssemblyName();

				if (EnvironmentHelper.IsLocal)
				{
					loggerConfiguration
						.WriteTo.Console();

					if (loggerConnectorModel.SeqHost.IsNotNullNorEmpty())
					{
						loggerConfiguration
							.WriteTo.Seq(loggerConnectorModel.SeqHost);
					}
				}
				else
				{
					loggerConfiguration
						.WriteTo.Console(new OpenSearchJsonFormatter());
				}

				advancedSinkRegistration?.Register(loggerConnectorModel, loggerConfiguration);
				moreConfigurationAction?.Invoke(serviceConnectorModel, loggerConnectorModel, loggerConfiguration);

				_isInitialized = true;
			}
		});

		return builder;
	}
}