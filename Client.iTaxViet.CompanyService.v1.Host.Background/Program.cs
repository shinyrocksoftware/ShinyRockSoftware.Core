using App.Rest.PostgreSql;
using Client.iTaxViet.CompanyService.v1.App.Background;
using Client.iTaxViet.CompanyService.v1.Background.BackgroundServices;
using Core.Job.Extensions;

var program = new RestPostgreSqlProgram();
program.Run(["Client"]
	, new()
	{
		UseKafka = true
	}
	, mvcBuilderDelegate: builder => builder.AddApplicationPart(Assembly.GetAssembly(typeof(AppBackgroundAnchor)))
	, extendingDelegate: (_, services) =>
	{
		services.AddPeriodicBackgroundService<CompanyCreatedBackgroundService>()
		        .AddPeriodicBackgroundService<CompanyUpdatedBackgroundService>()
		        .AddPeriodicBackgroundService<CompanyDeletedBackgroundService>();
	}
	, args: args);