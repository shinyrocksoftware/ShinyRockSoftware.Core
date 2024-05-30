using System.Text.Json.Serialization;
using Base.Extension;
using Core.Model.ApiErrors;
using Base.Model.Interface.ApiErrors;
using Base.Model.Interface.ApiResponses;

namespace Core.Model.ApiResponses;

public class ErrorApiResponse : ApiResponse, IErrorApiResponse
{
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public IList<IApiError>? Errors { get; internal set; }

	public ErrorApiResponse()
	{
		RequestId = Guid.NewGuid().ToString();
	}

	public ErrorApiResponse(string requestId)
	{
		RequestId = requestId;
	}

	public void SetError(string code, object message, string value = "")
	{
		if (code.IsNotNullNorEmpty())
		{
			Errors ??= new List<IApiError>();
			Errors.Add(new ApiError(code, message, value));
		}

		SetSuccess(false);
	}

	public void SetError(int code, object message, string value = "")
	{
		SetError(code.ToString(), message, value);
	}
}

public class ErrorApiResponse<T> : ErrorApiResponse, IErrorApiResponse<T>
{
	public T Data { get; set; }
}