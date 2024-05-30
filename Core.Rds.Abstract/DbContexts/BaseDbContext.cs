using Base.Extension;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Abstract.DbContexts;

public abstract class BaseDbContext<T>(DbContextOptions<T> options) : DbContext(options)
	where T : DbContext
{
	protected void BaseOnModelCreating(ModelBuilder modelBuilder, Type entityType)
	{
		var entityMethod = GetEntityMethod();

		if (entityMethod != null)
		{
			var types = new List<Type>();
			var assemblies = GetSafeAssemblies();
			var entityTypesCollection = assemblies.Select(assembly => assembly.GetTypes().Where(x => x.Namespace.IsNotNullNorEmpty() && x.IsSubclassOf(entityType) && x is { IsGenericType: false, IsAbstract: false }));

			foreach (var entityTypes in entityTypesCollection)
			{
				types.AddRange(entityTypes);
			}

			foreach (var type in types)
			{
				entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, []);
			}

			// ConfigureTypeConfiguration
			types.Clear();

			var typeConfigurations = assemblies.Select(assembly => assembly.GetTypes().Where(x => !x.IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))));

			foreach (var typeConfiguration in typeConfigurations)
			{
				types.AddRange(typeConfiguration);
			}

			foreach (var mappingType in types)
			{
				var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();
				var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);
				var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);
				var mapper = mappingType.CreateInstance(assemblies);

				mapper?.GetType()
				      .GetMethod("Configure")
				      ?.Invoke(mapper, [
					      entityBuilder
				      ]);
			}
		}

		base.OnModelCreating(modelBuilder);
	}

	private static IEnumerable<Assembly> GetSafeAssemblies()
	{
		return AppDomain.CurrentDomain.GetAssemblies()
		                .Where(c => c.FullName != null && !c.FullName.StartsWith("Microsoft")
		                                               && !c.FullName.StartsWith("System")
		                                               && !c.FullName.StartsWith("OpenId"));
	}

	private static MethodInfo? GetEntityMethod()
	{
		return typeof(ModelBuilder).GetMethods()
		                           .FirstOrDefault(x => x is { Name: "Entity", IsGenericMethod: true });
	}
}