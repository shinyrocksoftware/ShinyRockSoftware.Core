using Grpc.Net.Client;
using Lab.GrpcClient;

while (true)
{
	try
	{
		const string serverAddress = "http://localhost:5002";

		var channel = GrpcChannel.ForAddress(serverAddress);
		var client = new Greeter.GreeterClient(channel);

		var response = await client.SayHelloAsync(new()
		{
			Name = "World"
		});

		Console.WriteLine(response.Message);

		Thread.Sleep(2000);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
		Thread.Sleep(2000);
	}
}