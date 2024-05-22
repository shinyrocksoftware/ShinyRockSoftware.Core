using App.Rest.PostgreSql;

var program = new RestPostgreSqlProgram();
program.Run([]
	, new()
	, args: args);