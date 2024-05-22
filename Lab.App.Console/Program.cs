using System.Text.Json;
using Core.Extension;
using Core.Caching.Redis.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Core.App;
using Core.Caching.Interface;
using Core.Model.Interface;
using Core.Stream.Extensions;
using Core.Stream.Interface;
using Microsoft.Extensions.Logging;
using static System.Console;

namespace Lab.App.Console;

class Program
{
	// struct Test(int value)
	// {
	// 	public int Value { get; set; } = value;
	// };
	//
	// static int GetAgeIn(in Test test)
	// {
	// 	test.Value++;
	// 	return test.Value;
	// }
	// static int GetAgeRef(ref readonly Test test)
	// {
	// 	age++;
	// 	return age;
	// }

	static void Main(string[] args)
	{
		// return;

		// Kafka
		var serviceProvider = AppBuilder.CreateDefaultConsoleApplication(new [] {"Lab"}, (configuration, services) =>
		{
			services.AddKafka(configuration);
		}).Services;

		var logger = serviceProvider.GetRequiredService<ILogger>();

		var streamConsumer = serviceProvider.GetRequiredService<IStreamConsumer>();
		streamConsumer.Consume((string) "Group3", (string) "Client.iTaxViet.CompanyService.v1.App.Rest.Entity.Company.Created", (IStreamMessage streamMessage) =>
		{
			WriteLine(streamMessage.Value);
		});

		var streamProducer = serviceProvider.GetRequiredService<IStreamProducer>();

		// for (int i = 0; i < 10; i++)
		// {
		// 	streamProducer.Produce("User " + i, () =>
		// 	{
		// 		WriteLine("Produced " + i);
		// 	});
		// 	Thread.Sleep(2000);
		// }

		ReadKey();

		return;

		// Redis
		serviceProvider = AppBuilder.CreateDefaultConsoleApplication(new [] {"Lab"}, (configuration, services) =>
		{
			services.AddRedis(configuration);
		}).Services;

		var redisKeyValueRepository = serviceProvider.GetRequiredService<IRedisRepository>();
		redisKeyValueRepository.Set("Test", "123");

		var test = redisKeyValueRepository.GetAsync("Test")
		                                  .GetAwaiter()
		                                  .GetResult();
		WriteLine("Test: " + test);

		var jsonData = "{ \"success\": false }".Deserialize<JsonElement>();
		WriteLine(jsonData);
	}

	static async Task Main2(string[] args)
	{
		await Task.CompletedTask;

		IInterface obj = new MyClass();
		var interceptor = new MyInterceptor<IInterface>();
		obj = interceptor.Decorate(obj);

		var m1 = obj.Method(1, 2);
		WriteLine(m1);

		// var m2 = await obj.MethodAsync(1, 2);
		// WriteLine(m2);
	}
}

public interface IInterface
{
	object Method(object one, object two);
	Task<object> MethodAsync(object one, object two);
}

public class MyClass : IInterface
{
	public object Method(object one, object two)
	{
		WriteLine(one);
		WriteLine(two);
		// return "Method result";
		throw new("In Method exception");
	}

	public async Task<object> MethodAsync(object one, object two)
	{
		await Task.CompletedTask;
		WriteLine(one);
		WriteLine(two);
		return "MethodAsync result";
		throw new("In MethodAsync exception");
	}
}

//Interceptor class (every method there is optional):
public class MyInterceptor<T> : ClassInterceptor<T>
{
	protected override void OnInvoking(MethodInfo methodInfo, object[] args)
	{
		WriteLine("Invoking");
	}

	protected override void OnInvoked(MethodInfo methodInfo, object[] args, object result)
	{
		WriteLine("Invoked");
	}

	protected override void OnException(MethodInfo methodInfo, object[] args, Exception exception)
	{
		WriteLine("Exception");
	}
}

public abstract class ClassInterceptor<TInterface> : DispatchProxy
{
	private object _decorated;

	public TInterface Decorate<TImplementation>(TImplementation decorated)
		where TImplementation : TInterface
	{
		var interceptor = typeof(DispatchProxy)
		                  .GetMethod("Create")
		                  ?.MakeGenericMethod(typeof(TInterface), GetType())
		                  .Invoke(null, Array.Empty<object>());
		if (interceptor is ClassInterceptor<TInterface> proxy)
		{
			proxy._decorated = decorated;

			return (TInterface) (object) proxy;
		}

		return default;
	}

	protected override object Invoke(MethodInfo targetMethod, object[] args)
	{
		OnInvoking(targetMethod, args);

		try
		{
			var result = targetMethod.Invoke(_decorated, args);

			if (result is Task<object> resultTask)
			{
				resultTask
					.ContinueWith(task =>
					{
						if (task.IsFaulted)
						{
							OnException(targetMethod, args, task.Exception);
							result = Task.FromResult((object) null);
						}
						else
						{
							OnInvoked(targetMethod, args, result);
						}
					})
					.GetAwaiter()
					.GetResult();

				return result;
			}

			OnInvoked(targetMethod, args, result);
			return result;
		}
		catch (TargetInvocationException exc)
		{
			OnException(targetMethod, args, exc);
			return null;
		}
	}

	protected virtual void OnException(MethodInfo methodInfo, object[] args, Exception exception)
	{
	}

	protected virtual void OnInvoked(MethodInfo methodInfo, object[] args, object result)
	{
	}

	protected virtual void OnInvoking(MethodInfo methodInfo, object[] args)
	{
	}
}