using Core.Configuration.Extensions;
using Core.Constant;
using Core.Helper;
using Core.Totp.ConfigurationConnectors;
using Core.Totp.ConnectorModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Totp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTotp(this IServiceCollection services, IConfiguration configuration)
    {
        return EnvironmentHelper.IsLocal
            ? services.AddConnectorModelDependencies<TotpConnectorModel, TotpConfigurationConnector>(configuration, SettingConstants.APP_SETTING_TOTP)
            : services.AddConnectorModelDependencies<TotpConnectorModel, TotpRemoteConfigurationConnector>();
    }
}