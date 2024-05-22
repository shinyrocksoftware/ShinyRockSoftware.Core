using Core.Attribute;
using Core.Attribute.AutoInjection;
using Core.Configuration.ConfigurationConnectors;
using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;
using Core.Constant;
using Core.Extension;
using Core.Helper;
using Core.Model.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// The shorthand method to register default settings from these common services, includes:
	/// - ILogger
	/// - IConfigurationHelpers
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection AddServiceBase(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddConnectorModelDependencies<ServiceConnectorModel, ServicesConfigurationConnector>(configuration, SettingConstants.APP_SETTING_SERVICE)
		               .AddConnectorModelServices();
	}

	/// <summary>
	/// The shorthand method to register settings from these common services, includes:
	/// - DefaultConnectorModel registration
	/// - IConnectorModelHelpers
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddConnectorModelServices(this IServiceCollection services)
	{
		if (EnvironmentHelper.IsLocal)
		{
			services.TryAddSingleton<IConfigurationConnector, ConfigurationConnector>();
		}
		else
		{
			services.TryAddSingleton<IConfigurationConnector, RemoteConfigurationConnector>();
		}

		return services;
	}

	public static IServiceCollection AddConnectorModelDependencies<T, TV>(this IServiceCollection services, IConfiguration configuration, string sectionKey)
		where T : class, IConnectorModel, new()
	{
		return services.Configure<T>(configuration.GetSection(sectionKey).Bind)
		               .AddConnectorModelDependencies<T, TV>();
	}

	public static IServiceCollection AddConnectorModelDependencies<T, TV>(this IServiceCollection services)
		where T : IConnectorModel, new()
	{
		services.TryAddSingleton(typeof(IConfigurationConnector<T>), typeof(TV));

		return services;
	}

	public static IServiceCollection AutoInject(this IServiceCollection services, string[] appRootNamespaces, IEnumerable<Assembly>? assemblies = null)
	{
		var scanningAssemblies = assemblies == null ? new List<Assembly>
		{
			Assembly.GetCallingAssembly()
		} : new();

		string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		scanningAssemblies.AddRange(Directory.GetFiles(assemblyFolder, "*.dll").Select(Assembly.LoadFrom));

		var types = scanningAssemblies.SelectMany(s => s.GetTypes())
		                              .Where(c => c.FullName.StartsWithCI("Core")
		                                          || c.FullName.StartsWithCI("Client")
		                                          || c.FullName.StartsWithCI("Lab")
		                                          || c.FullName.StartsWithCI("Shared")
		                                          || (appRootNamespaces != null && appRootNamespaces.Any(d => c.FullName.StartsWithCI(d))));

		var classes = types.Where(p => p.IsClass && typeof(IAutoInjection).IsAssignableFrom(p));
		var interfaces = types.Where(p => p.IsInterface && typeof(IAutoInjection).IsAssignableFrom(p) && p.Name != nameof(IAutoInjection));

		Inject<SingletonAutoInjectionAttribute>(classes, interfaces, services.TryAddSingleton);
		Inject<ScopedAutoInjectionAttribute>(classes, interfaces, services.TryAddScoped);
		Inject<TransientAutoInjectionAttribute>(classes, interfaces, services.TryAddTransient);
		Inject<DecoratorAutoInjectionAttribute>(classes, interfaces, services.TryDecorate);

		return services;
	}

	public static T GetConnectorModel<T>(this IServiceCollection services)
		where T : class, IConnectorModel
	{
		var serviceProvider = services.BuildServiceProvider();
		var connectorModelHelper = serviceProvider.GetRequiredService<IConnectorModelHelper>();
		return connectorModelHelper.GetConnector<T>();
	}

	private static void Inject<T>(IEnumerable<Type> autoInjectionClasses, IEnumerable<Type> interfaces, Func<Type, Type, bool> action)
		where T : OrderAttribute
	{
		Inject<T>(autoInjectionClasses, interfaces, (service, implementation) =>
		{
			action(service, implementation);
		});
	}

	private static void Inject<T>(IEnumerable<Type> autoInjectionClasses, IEnumerable<Type> interfaces, Action<Type, Type> action)
		where T : OrderAttribute
	{
		var distinctClasses = new Dictionary<string, Type>();
		var classes = autoInjectionClasses.Where(p => p.GetCustomAttributes(typeof(T), true).Length > 0);

		foreach (var @class in classes.Distinct())
		{
			if (distinctClasses.All(c => c.Key != @class.Name))
			{
				distinctClasses.Add(@class.Name, @class);
			}
			else
			{
				var existedClass = distinctClasses[@class.Name];
				var existedClassAttributes = existedClass.GetCustomAttributes(typeof(T), true);
				var currentClassAttributes = @class.GetCustomAttributes(typeof(T), true);

				var existedClassAttribute = existedClassAttributes.FirstOrDefault();
				var currentClassAttribute = currentClassAttributes.FirstOrDefault();

				if (existedClassAttribute != null && currentClassAttribute != null)
				{
					var existedOrder = ((T) existedClassAttribute).Order;
					var currentOrder = ((T) currentClassAttribute).Order;

					if (existedOrder == currentOrder)
					{
						Console.WriteLine("=== ERROR ===");
						Console.WriteLine("Conflict orders between attributes of instances of {0}".ApplyFormat(@class.Name));
						Environment.Exit(0);
					}
					else
					{
						if (existedOrder > currentOrder)
						{
							distinctClasses[@class.Name] = @class;
						}
					}
				}
			}
		}

		foreach ((_, var value) in distinctClasses)
		{
			var @interface = interfaces.FirstOrDefault(c => c.IsAssignableFrom(value));

			if (@interface != null)
			{
				action(@interface, value);
			}
		}
	}
}