using Base.Extension;
using Core.Auth.DbRequests;
using Core.Auth.Entities;
using Core.Auth.Interfaces;
using Base.Model.Interface;
using Core.Rds.Abstract.Extensions;
using Core.Rds.DbContexts;
using Core.Rds.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Core.Auth.Repositories;

public class AspNetUserReadRepository(ILogger<AspNetUserReadRepository> logger, PlainReadDbContext dbContext)
	: PlainReadRepository<Guid, AspNetUser, AspNetUserDto>(logger, dbContext), IAspNetUserReadRepository
{
	protected override Func<IQueryable<AspNetUser>, IIncludableQueryable<AspNetUser, object>> DefaultIncludes { get; set; }

    public async ValueTask<IEnumerablePage<AspNetUserDto>> GetPagingUsersAsync(GetPagingUsersDbRequest dbRequest, CancellationToken cancellationToken)
    {
        var search = dbRequest.Search;

        if (search.IsNotNullNorEmpty())
        {
            search = search.Trim().ToLower();
        }

        return await Query()
                     .Where(user => search.IsNullOrEmpty() || user.Email != null && user.Email.ToLower().Contains(search))
                     .OrderBy(users => users.OrderByDescending(c => c.CreatedDate))
                     .SelectPagedAsync(dbRequest.PageNumber, dbRequest.PageSize, null, cancellationToken);
    }
}