using Core.Auth.Entities;
using Core.Extension;
using Core.Rds.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Core.Auth.Models;

public class ApplicationUserStore(ApplicationDbContext context, IPlainReadRepository<Guid, AspNetUserLogin, AspNetUserLoginDto> aspNetUserLoginReadRepository, IdentityErrorDescriber? describer = null)
    : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>(context, describer)
{
    public override async Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default)
    {
        var userLogin = await aspNetUserLoginReadRepository.GetAsync(c => c.LoginProvider == loginProvider && c.ProviderKey.EqualsCI(providerKey), null, null, null, cancellationToken);
        return userLogin == null
            ? null
            : new ApplicationUser
            {
                Id = userLogin.UserId
            };
    }
}