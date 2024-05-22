using Core.Auth.DbRequests;
using Core.Auth.Entities;
using Core.Model.Interface;
using Core.Rds.Interface;

namespace Core.Auth.Interfaces;

public interface IAspNetUserReadRepository : IPlainReadRepository<Guid, AspNetUser, AspNetUserDto>
{
	Task<IEnumerablePage<AspNetUserDto>> GetPagingUsersAsync(GetPagingUsersDbRequest dbRequest, CancellationToken cancellationToken);
}