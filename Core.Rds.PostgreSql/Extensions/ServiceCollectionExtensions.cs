using Core.Configuration.Extensions;
using Core.Rds.ConnectorModels;
using Core.Rds.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Rds.PostgreSql.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPostgreSqlRdsDb(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddRdsDbContext(new PostgreSqlRdsDbContext(), configuration);

		var healthChecksBuilder = services.AddHealthChecks();

		var rdsConnectorModel = services.GetConnectorModel<RdsConnectorModel>();
		healthChecksBuilder.AddNpgSql(rdsConnectorModel.ConnectionStringReader);

		return services;
	}
}