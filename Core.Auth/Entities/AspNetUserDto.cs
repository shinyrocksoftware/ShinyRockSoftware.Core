using Core.Auth.Models;
using Core.Model.Abstract.Entities;
using Base.Model.Interface.Entities;

namespace Core.Auth.Entities;

public class AspNetUserDto : BasePlainEntityDto<Guid>, IPlainEntityDto<Guid>
{
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public ICollection<AspNetUserClaimDto> UserClaims { get; } = new List<AspNetUserClaimDto>();
    public ICollection<AspNetUserRoleDto> UserRoles { get; } = new List<AspNetUserRoleDto>();
    public DateTime CreatedDate { get; set; }

    public ApplicationUser ToApplicationUser()
    {
        return new()
        {
            Id = Id
            , UserName = UserName
            , NormalizedUserName = NormalizedUserName
            , Email = Email
            , NormalizedEmail = NormalizedEmail
            , EmailConfirmed = EmailConfirmed
            , PasswordHash = PasswordHash
            , SecurityStamp = SecurityStamp
            , ConcurrencyStamp = ConcurrencyStamp
            , PhoneNumber = PhoneNumber
            , PhoneNumberConfirmed = PhoneNumberConfirmed
            , TwoFactorEnabled = TwoFactorEnabled
            , LockoutEnd = LockoutEnd
            , LockoutEnabled = LockoutEnabled
            , AccessFailedCount = AccessFailedCount
            ,
        };
    }
}