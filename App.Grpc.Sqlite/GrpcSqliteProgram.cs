using Core.Rds.Sqlite.Extensions;
using Microsoft.AspNetCore.Routing;
using App.Grpc.Abstract;
using Core.App;

namespace App.Grpc.Sqlite;

public class GrpcSqliteProgram : BaseGrpcProgram
{
	public void Run(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<IEndpointRouteBuilder> mapGrpcEndpointsDelegate
		, string[]? args = null)
	{
		RunDefault(appRootNamespaces, featureOptions, mapGrpcEndpointsDelegate, null, null, (configuration, services) =>
		{
			if (featureOptions.UseRds)
			{
				services.AddSqliteRdsDb(configuration);
			}
		}, args: args);
	}
}