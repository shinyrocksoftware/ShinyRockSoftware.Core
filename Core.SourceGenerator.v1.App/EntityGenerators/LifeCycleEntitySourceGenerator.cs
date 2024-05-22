using Core.SourceGenerator.v1.Generators;
using Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.App.EntityGenerators;

[Generator]
public class LifeCycleEntitySourceGenerator : BaseLifeCycleEntitySourceGenerator
{
	public override Type TemplateAnchor { get; set; } = typeof(Anchor);

	public override Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; } = () => new List<GeneratorRegistration>
	{
		new()
		{
			TemplateResourceFileName = "NotificationEventEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "{{EntityName}}{{EntityLifeCycleEvent}}NotificationEvent.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "NotificationHandlerEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "{{EntityName}}{{EntityLifeCycleEvent}}NotificationHandler.g.cs"
		}
	};
}