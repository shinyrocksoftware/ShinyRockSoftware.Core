using Core.Auth.DbRequests;
using Core.Auth.Entities;
using Base.Model.Interface;
using Core.Rds.Interface;

namespace Core.Auth.Interfaces;

public interface IAspNetUserReadRepository : IPlainReadRepository<Guid, AspNetUser, AspNetUserDto>
{
	ValueTask<IEnumerablePage<AspNetUserDto>> GetPagingUsersAsync(GetPagingUsersDbRequest dbRequest, CancellationToken cancellationToken);
}