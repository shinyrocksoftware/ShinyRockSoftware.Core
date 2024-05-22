using App.Rest.PostgreSql;

var program = new RestPostgreSqlProgram();
program.Run(["Shared"]
	, new()
	{
		UseKafka = true
		, UseRedis = true
		, UseRds = true
	}
	, args: args);