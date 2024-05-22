namespace Core.Model.Interface;

public interface IPagingResponseResult<T> : IResponseResult<IEnumerablePage<T>>
{
    new IEnumerablePage<T> Data { get; set; }
}