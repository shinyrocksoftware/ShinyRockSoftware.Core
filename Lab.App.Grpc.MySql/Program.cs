using App.Grpc.MySql;
using Lab.App.Grpc.MySql.Services;

var program = new GrpcMySqlProgram();
program.Run(["Lab"]
	, new()
	, endpoints =>
	{
		endpoints.MapGrpcService<GreeterService>();
	}, args: args);