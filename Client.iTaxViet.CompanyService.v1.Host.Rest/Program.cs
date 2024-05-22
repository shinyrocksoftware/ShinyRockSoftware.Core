using App.Rest.PostgreSql;
using Client.iTaxViet.CompanyService.v1.App.Rest;

var program = new RestPostgreSqlProgram();
program.Run(["Client"]
	, new()
	{
		UseKafka = true
		, UseRedis = true
		, UseRds = true
	}
	, mvcBuilderDelegate: builder => builder.AddApplicationPart(Assembly.GetAssembly(typeof(AppRestAnchor)))
	, args: args);