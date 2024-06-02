using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("AspNetUserClaims")]
public class AspNetUserClaim(Guid userId, string claimType, string claimValue) : BasePlainEntity<Guid>
{
    public Guid UserId { get; set; } = userId;
    public AspNetUser User { get; set; }
    public string ClaimType { get; set; } = claimType;
    public string ClaimValue { get; set; } = claimValue;
}