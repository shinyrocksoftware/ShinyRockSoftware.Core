using Base.Model.Interface;

namespace Core.Model.Abstract.ResponseModels;

public abstract class BasePagingResponseModel<T> : BaseResponseModel
{
    public IEnumerablePage<T> Data { get; set; }
}