using Base.Constant;
using Base.Extension;
using Core.Model.Abstract.Extensions;
using Core.Model.ApiResponses;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Attribute.ActionFilters;

public class PayloadValidationActionFilterAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
		{
			var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true);
			if (actionAttributes.Any(c => c.GetType() == typeof(SkipPreFlightValidationAttribute))) return;

			if (!context.ModelState.IsValid)
			{
				var messageBuilder = new StringBuilder();

				foreach (var error in from pair in context.ModelState
				                      where pair.Value.Errors.IsNotNullNorEmpty()
				                      from error in pair.Value.Errors
				                      where error.ErrorMessage.IsNotNullNorEmpty()
				                      select error)
				{
					messageBuilder.AppendLine(error.ErrorMessage);
				}

				var message = messageBuilder.ToString();
				message = message.IsNullOrEmpty()
					? "Error"
					: message;
				var errorApiResponse = new ErrorApiResponse(context.HttpContext.GetRequestId());
				errorApiResponse.SetError(GeneralConstants.CODE_FOOTPRINTS, message);
				errorApiResponse.SetMessage(message);

				var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<PayloadValidationActionFilterAttribute>>();
				logger.LogError(message, errorApiResponse);
				context.Result = errorApiResponse.BadRequestJsonData();
			}

			base.OnActionExecuting(context);
		}
	}
}