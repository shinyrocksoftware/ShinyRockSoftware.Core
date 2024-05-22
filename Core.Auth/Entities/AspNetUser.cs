using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;
using Core.Model.Interface.Entities;

namespace Core.Auth.Entities;

[Table("AspNetUsers")]
public class AspNetUser : BasePlainEntity<Guid>, IPlainEntity<Guid>
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
    public ICollection<AspNetUserClaim> UserClaims { get; } = new List<AspNetUserClaim>();
    public ICollection<AspNetUserRole> UserRoles { get; } = new List<AspNetUserRole>();
    public DateTime CreatedDate { get; set; }
}