using Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator;

[Generator]
public class LifeCycleEntitySourceGenerator : BaseLifeCycleEntitySourceGenerator
{
	public override Type TemplateAnchor { get; set; } = typeof(Anchor);

	public override Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; } = () => new List<GeneratorRegistration>
	{
		new()
		{
			TemplateResourceFileName = "StreamTopicsEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "{{EntityName}}{{EntityLifeCycleEvent}}StreamTopics.g.cs"
		}
	};
}