using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;
using Core.Doc.Swashbuckle.DocumentFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Core.Doc.Swashbuckle.Extensions;

public static class ServiceCollectionExtensions
{
	public static void RemoveOpenIdConnectRequestModel(this SwaggerGenOptions c)
	{
		c.DocumentFilter<OpenApiRemoveOpenIdConnectRequestModelDocumentFilter>();
	}

	public static void AddAuthorizerDefaultClient(this SwaggerUIOptions c, string clientId, string clientSecret)
	{
		c.OAuthClientId(clientId);
		c.OAuthClientSecret(clientSecret);
	}

	public static IServiceCollection AddDefaultSwagger(this IServiceCollection services, Action<SwaggerGenOptions, string>? extendingOptions = null)
	{
		services.AddSwaggerGen(c =>
		        {
			        var serviceProvider = services.BuildServiceProvider();
			        var connectorModelHelper = serviceProvider.GetRequiredService<IConnectorModelHelper>();
			        var serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();
			        var version = $"v{serviceConnectorModel.Version}";

			        c.SwaggerDoc(version, new()
			        {
				        Title = serviceConnectorModel.Name
				        , Version = version
			        });

			        string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
			        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			        c.IncludeXmlComments(xmlPath, true);

			        extendingOptions?.Invoke(c, xmlPath);
		        });

		return services;
	}
}