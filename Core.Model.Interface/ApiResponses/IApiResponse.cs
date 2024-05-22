namespace Core.Model.Interface.ApiResponses;

public interface IApiResponse
{
	string RequestId { get; set; }
	bool IsSuccess { get; set; }
	string Message { get; set; }
	void SetSuccess(bool isSuccess);
	void SetMessage(string message);
}

public interface IApiResponse<T> : IApiResponse
{
	T Data { get; set; }
}