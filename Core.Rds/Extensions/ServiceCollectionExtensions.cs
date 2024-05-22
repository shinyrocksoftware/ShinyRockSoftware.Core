using Core.Caching.Memory.Extensions;
using Core.Configuration.Extensions;
using Core.Configuration.Interface;
using Core.Constant;
using Core.Extension;
using Core.Helper;
using Core.Rds.ConfigurationConnectors;
using Core.Rds.ConnectorModels;
using Core.Rds.DbContexts;
using Core.Rds.Interface;
using Core.Rds.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Rds.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRdsReadWrite(this IServiceCollection services)
	{
		// Read
		services.TryAddScoped(typeof(IReadRepository<,,>), typeof(ReadRepository<,,>));
		services.TryAddScoped(typeof(IPlainReadRepository<,,>), typeof(PlainReadRepository<,,>));

		// Write
		services.TryAddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));
		services.TryAddScoped(typeof(IPlainWriteRepository<,>), typeof(PlainWriteRepository<,>));

		return services.AddInMemoryCache();
	}

	public static IServiceCollection AddRdsGenericRepositories<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> addDbContextAction)
		where T : DbContext
	{
		return services.AddDbContext<T>(options =>
		{
			addDbContextAction(options);

			#if DEBUG

			options.EnableSensitiveDataLogging();

			#endif
		}, ServiceLifetime.Transient);
	}

	public static RdsConnectorModel InitRdsConnectorModel(this IServiceCollection services, IConfiguration configuration)
	{
		services = EnvironmentHelper.IsLocal
			? services.AddConnectorModelDependencies<RdsConnectorModel, RdsConfigurationConnector>(configuration, SettingConstants.APP_SETTING_RDS)
			: services.AddConnectorModelDependencies<RdsConnectorModel, RdsRemoteConfigurationConnector>();

		var serviceProvider = services.BuildServiceProvider();
		var connectorModelHelper = serviceProvider.GetRequiredService<IConnectorModelHelper>();

		return connectorModelHelper.GetConnector<RdsConnectorModel>();
	}

	public static IServiceCollection AddRdsDbContext(this IServiceCollection services, IRdsDbContext rdsDbContext, IConfiguration configuration)
	{
		var rdsConnectorModel = services.InitRdsConnectorModel(configuration);

		return services
		       .AddRdsReadWrite()
		       .AddRdsGenericRepositories<WriteDbContext>(options => rdsDbContext.AddDbContext(options, rdsConnectorModel.Custom.IsNullOrEmpty()
			       ? rdsConnectorModel.ConnectionStringWriter
			       : rdsConnectorModel.Custom))
		       .AddRdsGenericRepositories<ReadDbContext>(options => rdsDbContext.AddDbContext(options, rdsConnectorModel.Custom.IsNullOrEmpty()
			       ? rdsConnectorModel.ConnectionStringReader
			       : rdsConnectorModel.Custom))
		       .AddRdsGenericRepositories<PlainWriteDbContext>(options => rdsDbContext.AddDbContext(options, rdsConnectorModel.Custom.IsNullOrEmpty()
			       ? rdsConnectorModel.ConnectionStringWriter
			       : rdsConnectorModel.Custom))
		       .AddRdsGenericRepositories<PlainReadDbContext>(options => rdsDbContext.AddDbContext(options, rdsConnectorModel.Custom.IsNullOrEmpty()
			       ? rdsConnectorModel.ConnectionStringReader
			       : rdsConnectorModel.Custom));
	}
}