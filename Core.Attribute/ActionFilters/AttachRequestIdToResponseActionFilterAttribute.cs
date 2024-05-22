using Core.Extension;
using Core.Model.Interface.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Attribute.ActionFilters;

public class AttachRequestIdToResponseActionFilterAttribute : ActionFilterAttribute
{
	public override void OnActionExecuted(ActionExecutedContext context)
	{
		if (context.Result is JsonResult { Value: IApiResponse response } jsonResult)
		{
			response.RequestId = context.HttpContext.GetRequestId();
			context.Result = jsonResult;
		}
	}
}