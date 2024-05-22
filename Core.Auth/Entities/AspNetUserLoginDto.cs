using Core.Model.Abstract.Entities;
using Core.Model.Interface.Entities;

namespace Core.Auth.Entities;

public class AspNetUserLoginDto : BasePlainEntityDto<Guid>, IPlainEntityDto<Guid>
{
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
    public string ProviderDisplayName { get; set; }
    public Guid UserId { get; set; }
}