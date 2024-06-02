using Microsoft.CodeAnalysis.CSharp;
using Scriban;

namespace Core.SourceGenerator.v1;

public static class TemplateRenderer
{
    public static string Parse(string templateString, object model)
    {
        var template = Template.Parse(templateString);

        if (template.HasErrors)
        {
            var errors = string.Join(" | ", template.Messages.Select(x => x.Message));
            throw new InvalidOperationException($"Template parse error: {errors}");
        }

        var result = template.Render(model, member => member.Name);
        return SyntaxFactory.ParseCompilationUnit(result).GetText().ToString();
    }

    public static string Execute(string templateName, object model, Type? assemblyType = null)
    {
        var templateString = ResourceReader.GetResource(templateName, assemblyType);

        return templateString == null
            ? string.Empty
            : Parse(templateString, model);
    }
}