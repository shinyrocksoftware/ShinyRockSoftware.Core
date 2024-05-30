namespace Base.Model.Interface;

public interface IBaseResponseResult
{
    bool Success { get; }
    IList<string> ErrorMessages { get; }
}

public interface IResponseResult<T>
{
    T Data { get; set; }
}