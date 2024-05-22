using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

public class AspNetUserClaimDto : BasePlainEntityDto<Guid>
{
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public Guid UserId { get; set; }
}