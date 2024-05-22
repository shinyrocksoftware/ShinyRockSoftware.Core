namespace Core.Auth.DbRequests;

public class GetPagingUsersDbRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }

    public GetPagingUsersDbRequest(int pageNumber, int pageSize, string search = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Search = search;
    }
}