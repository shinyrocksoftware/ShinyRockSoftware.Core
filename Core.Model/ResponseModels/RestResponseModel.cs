using Core.Model.Abstract.ResponseModels;

namespace Core.Model.ResponseModels;

public class RestResponseModel<T> : BaseResponseModel<T>
{
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string ReasonPhase { get; set; }
}