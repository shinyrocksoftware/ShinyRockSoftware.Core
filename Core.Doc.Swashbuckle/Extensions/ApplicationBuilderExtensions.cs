using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;
using Core.Doc.Swashbuckle.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Core.Doc.Swashbuckle.Extensions;

public static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseOpenApiConverterAsJson(this IApplicationBuilder app, ILoggerFactory loggerFactory, string versionId)
	{
		return app.UseMiddleware<OpenApiConverterMiddleware>(loggerFactory, versionId, OpenApiFormat.Json);
	}

	public static IApplicationBuilder UseOpenApiConverterAsYaml(this IApplicationBuilder app, ILoggerFactory loggerFactory, string versionId)
	{
		return app.UseMiddleware<OpenApiConverterMiddleware>(loggerFactory, versionId, OpenApiFormat.Yaml);
	}

	public static IApplicationBuilder UseOpenApiConverterAsJson(this IApplicationBuilder app, ILogger logger, string versionId)
	{
		return app.UseMiddleware<OpenApiConverterMiddleware>(logger, versionId, OpenApiFormat.Json);
	}

	public static IApplicationBuilder UseOpenApiConverterAsYaml(this IApplicationBuilder app, ILogger logger, string versionId)
	{
		return app.UseMiddleware<OpenApiConverterMiddleware>(logger, versionId, OpenApiFormat.Yaml);
	}

	public static IApplicationBuilder UseDefaultSwagger(this IApplicationBuilder app)
	{
		var services = app.ApplicationServices;
		var connectorModelHelper = services.GetService<IConnectorModelHelper>();
		var serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();

		return app.UseSwagger(c =>
		{
			c.PreSerializeFilters.Add((document, request) =>
			{
				var paths = document.Paths.ToDictionary(item =>
				{
					var finalKey = new StringBuilder();

					var segments = item.Key.Split('/');

					for (var i = 0; i < segments.Length; i++)
					{
						var segment = segments[i];
						finalKey.Append($"{(segment.StartsWith("{") && segment.EndsWith("}") ? segment : segment.ToLowerInvariant())}{(i == segments.Length - 1 ? string.Empty : "/")}");
					}

					return finalKey.ToString();
				}, item => item.Value);

				document.Paths.Clear();

				foreach ((var key, var value) in paths)
				{
					document.Paths.Add(key, value);
				}
			});
		}).UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint($"/swagger/v{serviceConnectorModel.Version}/swagger.json", serviceConnectorModel.Version);
			c.DocExpansion(DocExpansion.List);
		});
	}
}