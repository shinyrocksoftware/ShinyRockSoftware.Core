using Core.Constant;
using Core.Model.Interface.ApiErrors;

namespace Core.Model.ApiErrors;

public class ApiError(string code, object message, string value) : IApiError
{
	public string Code { get; set; } = code;
	public object Message { get; set; } = message;
	public string Value { get; set; } = value;

	public ApiError(string message) : this(GeneralConstants.CODE_GENERAL_ERROR, "", message)
	{
	}
}