using Core.Constant;
using Core.Extension;
using Core.Model.Abstract.Extensions;
using Core.Model.ApiResponses;
using Core.Model.Interface.ApiRequests;
using Core.Model.Interface.ApiResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Attribute.ActionFilters;

public class DateTimeUtcValidationActionFilterAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
		{
			var httpMethod = controllerActionDescriptor.ActionConstraints?.FirstOrDefault(c => c.GetType() == typeof(HttpMethodActionConstraint)) as HttpMethodActionConstraint;

			var methods = new[]
			{
				HttpMethods.Post
				, HttpMethods.Put
				, HttpMethods.Delete
			};

			if (methods.Contains(httpMethod?.HttpMethods.First()))
			{
				foreach (var argument in context.ActionArguments)
				{
					if (argument.Value is IApiRequest)
					{
						var properties = argument.Value.GetType().GetProperties();

						var invalidPropertyNames = (from propertyInfo in properties
						                            where propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?)
						                            let dateTime = (DateTime) propertyInfo.GetValue(argument.Value, null)!
						                            where dateTime.Kind != DateTimeKind.Utc
						                            select propertyInfo.Name).ToList();

						if (invalidPropertyNames.Any())
						{
							var message = $"The \"{string.Join("\", \"", invalidPropertyNames)}\" must be in UTC format";
							IErrorApiResponse errorApiResponse = new ErrorApiResponse(context.HttpContext.GetRequestId());
							errorApiResponse.SetError(GeneralConstants.CODE_FOOTPRINTS, message);
							errorApiResponse.SetMessage(message);

							var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
							logger.LogInformation(message, errorApiResponse);
							context.Result = errorApiResponse.BadRequestJsonData();
						}
					}
				}
			}
		}
	}
}