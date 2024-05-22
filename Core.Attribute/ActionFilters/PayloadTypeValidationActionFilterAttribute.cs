using Core.Constant;
using Core.Extension;
using Core.Model.Abstract.Extensions;
using Core.Model.ApiResponses;
using Core.Model.Interface.ApiRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Attribute.ActionFilters;

public class PayloadTypeValidationActionFilterAttribute : ActionFilterAttribute
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
				, HttpMethods.Patch
			};

			if (!methods.Contains(httpMethod?.HttpMethods.First())) return;

			if (context.ActionArguments.Any())
			{
				if (!context.ActionArguments.All(c => c.Value is not IApiRequest)) return;

				var message = "Payload is missing or invalid";
				var errorApiResponse = new ErrorApiResponse(context.HttpContext.GetRequestId());
				errorApiResponse.SetError(GeneralConstants.CODE_FOOTPRINTS, message);
				errorApiResponse.SetMessage(message);

				var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
				logger.LogError(message, errorApiResponse);
				context.Result = errorApiResponse.BadRequestJsonData();
			}
		}
	}
}