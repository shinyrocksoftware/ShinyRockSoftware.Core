using App.Grpc.PostgreSql;
using Lab.App.Grpc.PostgreSql.Services;

var program = new GrpcPostgreSqlProgram();
program.Run(["Lab"]
	, new()
	, endpoints =>
	{
		endpoints.MapGrpcService<GreeterService>();
	}, args: args);