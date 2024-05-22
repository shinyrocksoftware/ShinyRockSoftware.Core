using Core.Rds.MySql.Extensions;
using Microsoft.AspNetCore.Routing;
using App.Grpc.Abstract;
using Core.App;

namespace App.Grpc.MySql;

public class GrpcMySqlProgram : BaseGrpcProgram
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
				services.AddMySqlRdsDb(configuration);
			}
		}, args: args);
	}
}