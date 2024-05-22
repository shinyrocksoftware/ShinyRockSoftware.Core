using Core.Extension;
using Core.Constant;
using Core.Model.ApiResponses;
using Core.Model.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.App.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
	    var hasError = false;
	    var logApiResponse = new ErrorApiResponse(context.TraceIdentifier);

	    try
	    {
		    await next(context);
	    }
	    catch (NullReferenceException ex)
	    {
		    hasError = true;
		    LogContext(logger, context, logApiResponse, ex, StatusCodes.Status404NotFound);
	    }
	    catch (Exception ex)
	    {
		    hasError = true;
		    LogContext(logger, context, logApiResponse, ex, StatusCodes.Status500InternalServerError);
	    }

	    if (hasError)
	    {
		    await context.Response.WriteAsync(logApiResponse.Serialize());
	    }
    }

    private void LogContext(ILogger logger, HttpContext context, ErrorApiResponse logApiResponse, Exception ex, int statusCode)
    {
	    logApiResponse.SetError(GeneralConstants.CODE_FOOTPRINTS, ex.GetExceptionDetails());
	    logApiResponse.SetMessage(ex.Message);
	    logger.LogCritical(ex.Message, logApiResponse);

	    context.Response.ContentType = WebConstants.CONTENT_TYPE_JSON;
	    context.Response.StatusCode = statusCode;
    }
}