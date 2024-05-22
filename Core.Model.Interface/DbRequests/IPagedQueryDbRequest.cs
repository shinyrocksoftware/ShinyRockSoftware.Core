namespace Core.Model.Interface.DbRequests;

public interface IPagedQueryDbRequest<T>
{
	public int PageSize { get; set; }
	public int PageNumber { get; set; }
}