namespace Core.Auth.DbRequests;

public class GetPagingUsersDbRequest(int pageNumber, int pageSize, string search = "")
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public string Search { get; set; } = search;
}