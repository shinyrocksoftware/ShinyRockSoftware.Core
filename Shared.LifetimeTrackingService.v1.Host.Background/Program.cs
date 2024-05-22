using App.Rest.PostgreSql;
using Core.Job.Extensions;
using Shared.LifetimeTrackingService.v1.App.Background;
using Shared.LifetimeTrackingService.v1.App.Background.BackgroundServices;

var program = new RestPostgreSqlProgram();
program.Run(["Shared"]
	, new()
	{
		UseKafka = true
		, UseRds = true
	}
	, mvcBuilderDelegate: builder => builder.AddApplicationPart(Assembly.GetAssembly(typeof(AppBackgroundAnchor)))
	, extendingDelegate: (_, services) =>
	{
		services.AddPeriodicBackgroundService<EntityLifetimeBackgroundService>();
	}
	, args: args);