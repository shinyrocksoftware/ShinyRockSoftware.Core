using Base.Model.Interface;

namespace Core.Model.Abstract.ResponseModels;

public abstract class BaseResponseModel : IBaseResponseResult
{
	public bool Success { get; set; }
	public IList<string> ErrorMessages { get; } = new List<string>();

	public void SetErrorMessage(string errorMessage)
	{
		Success = false;
		ErrorMessages.Add(errorMessage);
	}
}

public abstract class BaseResponseModel<T> : BaseResponseModel, IResponseResult<T>
{
	public T Data { get; set; }
}