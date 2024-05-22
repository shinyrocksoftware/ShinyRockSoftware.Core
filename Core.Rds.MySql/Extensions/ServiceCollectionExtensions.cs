using Core.Configuration.Extensions;
using Core.Rds.ConnectorModels;
using Core.Rds.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Rds.MySql.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddMySqlRdsDb(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddRdsDbContext(new MySqlRdsDbContext(), configuration);

		var healthChecksBuilder = services.AddHealthChecks();

		var rdsConnectorModel = services.GetConnectorModel<RdsConnectorModel>();
		healthChecksBuilder.AddMySql(rdsConnectorModel.ConnectionStringReader);

		return services;
	}
}