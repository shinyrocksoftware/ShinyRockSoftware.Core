using Core.Model.Interface.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace Core.Model.Abstract.Extensions;

public static class ApiResponseExtensions
{
    #region Success 200

    public static IActionResult SuccessJsonData(this IApiResponse response)
    {
        response.SetSuccess(true);
        return JsonData(response, 200);
    }

    #endregion

    #region BadRequest 400

    public static IActionResult BadRequestJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 400, errorCode, errorMessage);
    }

    public static IActionResult BadRequestJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 400, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Unauthorized 401

    public static IActionResult UnauthorizedJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 401, errorCode, errorMessage);
    }

    public static IActionResult UnauthorizedJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 401, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 403

    public static IActionResult ForbiddenJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 403, errorCode, errorMessage);
    }

    public static IActionResult ForbiddenJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 403, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotFound 404

    public static IActionResult NotFoundJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 404, errorCode, errorMessage);
    }

    public static IActionResult NotFoundJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 404, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotAcceptable 406

    public static IActionResult NotAcceptableJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 406, errorCode, errorMessage);
    }

    public static IActionResult NotAcceptableJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 406, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Internal Server 500

    public static IActionResult InternalServerJsonData(this IErrorApiResponse response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 500, errorCode, errorMessage);
    }

    public static IActionResult InternalServerJsonData(this IErrorApiResponse response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 500, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Failed

    public static IActionResult FailedJsonData(this IErrorApiResponse response, int status, string errorCode = "", string errorMessage = "")
    {
        response.SetError(errorCode, errorMessage);
        return JsonData(response, status);
    }

    public static IActionResult FailedJsonData(this IErrorApiResponse response, int status, string errorCode, string errorMessage, string errorValue)
    {
        response.SetError(errorCode, errorMessage, errorValue);
        return JsonData(response, status);
    }

    #endregion

    #region Generic functions

    #region Success 200

    public static IActionResult SuccessJsonData<T>(this IApiResponse<T> response, object? data = null, object? message = null)
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

    public static IActionResult BadRequestJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 400, errorCode, errorMessage);
    }

    public static IActionResult BadRequestJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 400, errorCode, errorMessage, errorValue);
    }

    public static IActionResult BadRequestJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(400, response, data, errorCode, errorMessage);
    }

    public static IActionResult BadRequestJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(400, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Unauthorized 401

    public static IActionResult UnauthorizedJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 401, errorCode, errorMessage);
    }

    public static IActionResult UnauthorizedJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 401, errorCode, errorMessage, errorValue);
    }

    public static IActionResult UnauthorizedJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(401, response, data, errorCode, errorMessage);
    }

    public static IActionResult UnauthorizedJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(401, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 403

    public static IActionResult ForbiddenJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 403, errorCode, errorMessage);
    }

    public static IActionResult ForbiddenJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 403, errorCode, errorMessage, errorValue);
    }

    public static IActionResult ForbiddenJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(403, response, data, errorCode, errorMessage);
    }

    public static IActionResult ForbiddenJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(403, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region NotFound 404

    public static IActionResult NotFoundJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 404, errorCode, errorMessage);
    }

    public static IActionResult NotFoundJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 404, errorCode, errorMessage, errorValue);
    }

    public static IActionResult NotFoundJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(404, response, data, errorCode, errorMessage);
    }

    public static IActionResult NotFoundJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(404, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 406

    public static IActionResult NotAcceptableJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 406, errorCode, errorMessage);
    }

    public static IActionResult NotAcceptableJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 406, errorCode, errorMessage, errorValue);
    }

    public static IActionResult NotAcceptableJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(406, response, data, errorCode, errorMessage);
    }

    public static IActionResult NotAcceptableJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(406, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Forbidden 500

    public static IActionResult InternalServerJsonData<T>(this IErrorApiResponse<T> response, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(response, 500, errorCode, errorMessage);
    }

    public static IActionResult InternalServerJsonData<T>(this IErrorApiResponse<T> response, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(response, 500, errorCode, errorMessage, errorValue);
    }

    public static IActionResult InternalServerJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        return FailedJsonData(500, response, data, errorCode, errorMessage);
    }

    public static IActionResult InternalServerJsonData<T>(this IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        return FailedJsonData(500, response, data, errorCode, errorMessage, errorValue);
    }

    #endregion

    #region Fail

    public static IActionResult FailedJsonData<T>(this IErrorApiResponse<T> response, int status, string errorCode = "", string errorMessage = "")
    {
        response.SetError(errorCode, errorMessage);
        return JsonData(response, status);
    }

    public static IActionResult FailedJsonData<T>(this IErrorApiResponse<T> response, int status, string errorCode, string errorMessage, string errorValue)
    {
        response.SetError(errorCode, errorMessage, errorValue);
        return JsonData(response, status);
    }

    public static IActionResult FailedJsonData<T>(int status, IErrorApiResponse<T> response, T data, string errorCode = "", string errorMessage = "")
    {
        response.Data = data;
        return FailedJsonData(response, status, errorCode, errorMessage);
    }

    public static IActionResult FailedJsonData<T>(int status, IErrorApiResponse<T> response, T data, string errorCode, string errorMessage, string errorValue)
    {
        response.Data = data;
        return FailedJsonData(response, status, errorCode, errorMessage, errorValue);
    }

    #endregion

    #endregion

    public static IActionResult JsonData(this IApiResponse response, int status)
    {
        return new JsonResult(response)
        {
            StatusCode = status
        };
    }
}