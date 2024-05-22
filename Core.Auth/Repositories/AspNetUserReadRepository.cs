using Core.Auth.DbRequests;
using Core.Auth.Entities;
using Core.Auth.Interfaces;
using Core.Extension;
using Core.Model.Interface;
using Core.Rds.DbContexts;
using Core.Rds.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Core.Auth.Repositories;

public class AspNetUserReadRepository(ILogger<AspNetUserReadRepository> logger, PlainReadDbContext dbContext)
	: PlainReadRepository<Guid, AspNetUser, AspNetUserDto>(logger, dbContext), IAspNetUserReadRepository
{
	protected override Func<IQueryable<AspNetUser>, IIncludableQueryable<AspNetUser, object>> DefaultIncludes { get; set; }

    public async Task<IEnumerablePage<AspNetUserDto>> GetPagingUsersAsync(GetPagingUsersDbRequest dbRequest, CancellationToken cancellationToken)
    {
        var search = dbRequest.Search;

        if (search.IsNotNullNorEmpty())
        {
            search = search.Trim().ToLower();
        }

        return await GetPagedAsync(
	        dbRequest.PageNumber
	        , dbRequest.PageSize
	        , user => search.IsNullOrEmpty() || user.Email != null && user.Email.ToLower().Contains(search)
	        , users => users.OrderByDescending(c => c.CreatedDate)
	        , null, null, cancellationToken);
    }
}