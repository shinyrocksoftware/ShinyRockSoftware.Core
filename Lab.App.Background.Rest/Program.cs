using App.Rest.PostgreSql;
using Core.BackgroundService.Extensions;
using Lab.App.Background.Rest.BackgroundServices;

var program = new RestPostgreSqlProgram();
program.Run(["Lab"]
	, new()
	, null
	, null
	, (_, services) =>
	{
		services.AddPeriodicBackgroundService<DefaultBackgroundService>();
	}
	, args: args);