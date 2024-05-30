using System.Text.RegularExpressions;
using Base.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Core.App.ModelConventions;

public partial class KebabCaseRoutingConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		foreach (var controllerAction in controller.Actions)
		{
			var actionName = controllerAction.ActionName;
			var selectors = controllerAction.Selectors;
			var controllerName = controller.Attributes.FirstOrDefault(c => c is RouteAttribute) is RouteAttribute routeAttribute ? routeAttribute.Template : controller.ControllerName;
			var kebabController = ConvertPascalToKebabCase(controllerName);

			foreach (var selector in selectors)
			{
				var template = new StringBuilder("~");
				template.Append($"/{kebabController}");

				if (actionName == "OPTIONS")
				{
					if (selector.AttributeRouteModel == null)
					{
						template.Append($"/{ConvertPascalToKebabCase(actionName)}");
					}
					else
					{
						AssignTemplate(template, selector);
					}
				}
				else
				{
					if (selector.AttributeRouteModel != null)
					{
						AssignTemplate(template, selector);
					}
				}

				template = template.Replace("[controller]", kebabController);

				if (selector.AttributeRouteModel == null)
				{
					selector.AttributeRouteModel = new()
					{
						Template = template.ToString()
					};
				}
				else
				{
					selector.AttributeRouteModel.Template = template.ToString();
				}
			}
		}
	}

	private static void AssignTemplate(StringBuilder template, SelectorModel selector)
	{
		if (selector.AttributeRouteModel != null)
		{
			if (selector.AttributeRouteModel.IsAbsoluteTemplate)
			{
				template.Clear();
				template.Append(ConvertPascalToKebabCase(selector.AttributeRouteModel.Template));
			}
			else
			{
				template.Append($"/{ConvertPascalToKebabCase(selector.AttributeRouteModel.Template).SafeEndpoint()}");
			}
		}
	}

	private static string ConvertPascalToKebabCase(string value)
	{
		var result = new StringBuilder(value);

		if (value.IsNotNullNorEmpty())
		{
			result.Clear();

			var arr = value.Split('/');

			for (var i = 0; i < arr.Length; i++)
			{
				var param = arr[i];

				result.Append(param.StartsWith('{') && param.EndsWith('}')
					? param
					: MyRegex().Replace(param, "-$1")
                           .Trim()
					       .ToLower());

				if (i < arr.Length - 1)
				{
					result.Append('/');
				}
			}
		}

		return result.ToString();
	}

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}