using Core.Model.Interface;

namespace Core.Model;

public class EnumerablePage<T> : IEnumerablePage<T>
{
    public int PageCount
    {
        get
        {
            var pageSize = Math.Max(1, PageSize);
            var pageCount = TotalCount / pageSize + (TotalCount % pageSize == 0 ? 0 : 1);
            return pageCount == 0 ? 1 : pageCount;
        }
    }

    public int PageIndex => PageNumber - 1;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T> PageData { get; set; }
    public IEnumerable<int> PageList { get; set; }
}