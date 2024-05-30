using Base.Model.Interface.ApiErrors;

namespace Base.Model.Interface.ApiResponses;

public interface IErrorApiResponse : IApiResponse
{
	IList<IApiError>? Errors { get; }
	void SetError(string code, object message, string value = "");
	void SetError(int code, object message, string value = "");
}

public interface IErrorApiResponse<T> : IErrorApiResponse
{
	T Data { get; set; }
}