using Core.Configuration.Extensions;
using Core.Rds.ConnectorModels;
using Core.Rds.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Rds.Sqlite.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSqliteRdsDb(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddRdsDbContext(new SqliteRdsDbContext(), configuration);

		var healthChecksBuilder = services.AddHealthChecks();

		var rdsConnectorModel = services.GetConnectorModel<RdsConnectorModel>();
		healthChecksBuilder.AddSqlite(rdsConnectorModel.ConnectionStringReader);

		return services;
	}
}