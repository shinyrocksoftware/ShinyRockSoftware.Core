using Core.Rds.SqlServer.Extensions;
using App.Rest.Abstract;
using Core.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Rest.SqlServer;

public class RestSqlServerProgram : BaseRestProgram
{
    public void Run(
        string[] appRootNamespaces
        , FeatureOptions featureOptions
        , Action<MvcOptions>? mvcOptionsDelegate = null
        , Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
        , Action<IConfiguration, IServiceCollection>? extendingDelegate = null
        , Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
        , int restPort = 5000
        , string[]? args = null
    )
    {
        RunDefault(
            appRootNamespaces
            , featureOptions
            , mvcOptionsDelegate
            , mvcBuilderDelegate
            , (configuration, services) =>
            {
                if (featureOptions.UseRds)
                {
                    services.AddSqlServerRdsDb(configuration);
                }

                extendingDelegate?.Invoke(configuration, services);
            }
            , customHealthChecksDelegate
            , restPort
            , args: args);
    }
}