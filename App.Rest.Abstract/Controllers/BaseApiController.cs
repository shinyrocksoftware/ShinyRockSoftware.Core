using Core.Model.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace App.Rest.Abstract.Controllers;

[ApiController]
public abstract class BaseApiController(ILogger<BaseApiController> logger) : ControllerBase
{
    protected string? AcceptLanguage => Request.Headers.ContainsKey(HeaderNames.AcceptLanguage) ? Request.Headers[HeaderNames.AcceptLanguage] : HeaderNames.AcceptLanguage;
    protected readonly ILogger Logger = logger;

    protected IActionResult ReturnSuccess<T>(T data)
    {
        return SuccessJsonData(new ApiResponse<T>
        {
            Data = data
        });
    }

    protected IActionResult ReturnInternalServer()
    {
        return InternalServerJsonData(new());
    }

    protected IActionResult ReturnNotFound()
    {
        return NotFoundJsonData(new());
    }

    #region Success 200

    private IActionResult SuccessJsonData(ApiResponse response)
    {
        response.SetSuccess(true);
        return JsonData(response, 200);
    }

    #endregion

    #region BadRequest 400

    private IActionResult BadRequestJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(400, response, errorCode, errorMessage);
    }

    private IActionResult BadRequestJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(400, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Unauthorized 401

    private IActionResult UnauthorizedJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(401, response, errorCode, errorMessage);
    }

    private IActionResult UnauthorizedJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(401, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 403

    private IActionResult ForbiddenJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(403, response, errorCode, errorMessage);
    }

    private IActionResult ForbiddenJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(403, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotFound 404

    private IActionResult NotFoundJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(404, response, errorCode, errorMessage);
    }

    private IActionResult NotFoundJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(404, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotAcceptable 406

    private IActionResult NotAcceptableJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(406, response, errorCode, errorMessage);
    }

    private IActionResult NotAcceptableJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(406, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Internal Server 500

    private IActionResult InternalServerJsonData(ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(500, response, errorCode, errorMessage);
    }

    private IActionResult InternalServerJsonData(ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(500, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Failed

    private IActionResult FailedJsonData(int status, ErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        response.SetError(errorCode, errorMessage);
        return JsonData(response, status);
    }

    private IActionResult FailedJsonData(int status, ErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        response.SetError(errorCode, errorMessage, errorValue);
        return JsonData(response, status);
    }

    #endregion

    #region Generic functions

    #region Success 200

    private IActionResult SuccessJsonData<T>(ApiResponse<T> response, object? data = null, object? message = null)
    {
        response.SetSuccess(true);

        if (data != null)
        {
            response.Data = (T) data;
        }

        if (message != null)
        {
            response.SetMessage(message.ToString());
        }

        return JsonData(response, 200);
    }

    #endregion

    #region BadRequest 400

    private IActionResult BadRequestJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(400, response, errorCode, errorMessage);
    }

    private IActionResult BadRequestJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(400, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult BadRequestJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(400, response, data, errorCode, errorMessage);
    }

    private IActionResult BadRequestJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(400, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Unauthorized 401

    private IActionResult UnauthorizedJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(401, response, errorCode, errorMessage);
    }

    private IActionResult UnauthorizedJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(401, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult UnauthorizedJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(401, response, data, errorCode, errorMessage);
    }

    private IActionResult UnauthorizedJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(401, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 403

    private IActionResult ForbiddenJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(403, response, errorCode, errorMessage);
    }

    private IActionResult ForbiddenJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(403, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult ForbiddenJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(403, response, data, errorCode, errorMessage);
    }

    private IActionResult ForbiddenJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(403, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotFound 404

    private IActionResult NotFoundJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(404, response, errorCode, errorMessage);
    }

    private IActionResult NotFoundJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(404, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult NotFoundJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(404, response, data, errorCode, errorMessage);
    }

    private IActionResult NotFoundJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(404, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 406

    private IActionResult NotAcceptableJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(406, response, errorCode, errorMessage);
    }

    private IActionResult NotAcceptableJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(406, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult NotAcceptableJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(406, response, data, errorCode, errorMessage);
    }

    private IActionResult NotAcceptableJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(406, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 500

    private IActionResult InternalServerJsonData<T>(ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(500, response, errorCode, errorMessage);
    }

    private IActionResult InternalServerJsonData<T>(ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(500, response, errorCode, errorMessage, errorValue);
    }

    private IActionResult InternalServerJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(500, response, data, errorCode, errorMessage);
    }

    private IActionResult InternalServerJsonData<T>(ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(500, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Fail

    private IActionResult FailedJsonData<T>(int status, ErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        response.SetError(errorCode, errorMessage);
        return JsonData(response, status);
    }

    private IActionResult FailedJsonData<T>(int status, ErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        response.SetError(errorCode, errorMessage, errorValue);
        return JsonData(response, status);
    }

    private IActionResult FailedJsonData<T>(int status, ErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        response.Data = data;
        return FailedJsonData(status, response, errorCode, errorMessage);
    }

    private IActionResult FailedJsonData<T>(int status, ErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        response.Data = data;
        return FailedJsonData(status, response, errorCode, errorMessage, errorValue);
    }

    #endregion

    #endregion

    private IActionResult JsonData(ApiResponse response, int status)
    {
        return new JsonResult(response)
        {
            StatusCode = status
        };
    }

    private IActionResult JsonData(ErrorApiResponse response, int status)
    {
        return new JsonResult(response)
        {
            StatusCode = status
        };
    }
}