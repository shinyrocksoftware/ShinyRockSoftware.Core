using Core.Configuration.Extensions;
using Core.Rds.ConnectorModels;
using Core.Rds.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Rds.SqlServer.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSqlServerRdsDb(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddRdsDbContext(new SqlServerRdsDbContext(), configuration);

		var healthChecksBuilder = services.AddHealthChecks();

		var rdsConnectorModel = services.GetConnectorModel<RdsConnectorModel>();
		healthChecksBuilder.AddSqlServer(rdsConnectorModel.ConnectionStringReader);

		return services;
	}
}