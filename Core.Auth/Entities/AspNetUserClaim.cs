using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("AspNetUserClaims")]
public class AspNetUserClaim : BasePlainEntity<Guid>
{
    public Guid UserId { get; set; }
    public AspNetUser User { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }

    public AspNetUserClaim(Guid userId, string claimType, string claimValue)
    {
        UserId = userId;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}