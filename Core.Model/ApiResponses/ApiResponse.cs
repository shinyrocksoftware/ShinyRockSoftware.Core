using System.Text.Json.Serialization;
using Base.Model.Interface.ApiResponses;

namespace Core.Model.ApiResponses;

public class ApiResponse : IApiResponse
{
	public string RequestId { get; set; }
	public bool IsSuccess { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string Message { get; set; }

	public void SetSuccess(bool isSuccess)
	{
		IsSuccess = isSuccess;
	}

	public void SetMessage(string message)
	{
		Message = message;
	}
}

public class ApiResponse<T> : ApiResponse, IApiResponse<T>
{
	public T Data { get; set; }
}