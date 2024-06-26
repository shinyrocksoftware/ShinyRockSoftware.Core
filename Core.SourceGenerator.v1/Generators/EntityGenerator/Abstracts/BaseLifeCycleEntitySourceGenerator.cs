using Core.SourceGenerator.v1.Generators.EntityGenerator.Models;
using Core.SourceGenerator.v1.Models;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;

public abstract class BaseLifeCycleEntitySourceGenerator : BaseEntitySourceGenerator
{
	public override void GenerateActionDelegate(IList<GeneratorRegistration> generatorRegistrations, GeneratorExecutionContext context, SourceModel model)
	{
		if (model is not EntityModel entityModel) throw new ApplicationException("model is not EntityModel");

		foreach (var @event in EntityLifeCycleConstants.Names)
		{
			entityModel.EntityLifeCycleEvent = @event;

			base.GenerateActionDelegate(generatorRegistrations, context, entityModel);
		}
	}
}