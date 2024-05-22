namespace Core.Model.Interface;

public interface IEnumerablePage<T>
{
	int PageCount { get; }
	int PageIndex { get; }
	int PageNumber { get; set; }
	int PageSize { get; set; }
	int TotalCount { get; set; }
	IEnumerable<T> PageData { get; set; }
	IEnumerable<int> PageList { get; set; }
}