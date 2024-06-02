using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

public class AspNetRoleDto : BasePlainEntityDto<Guid>
{
    public string ConcurrencyStamp { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public IEnumerable<AspNetUserRole> UserRoleDetails { get; } = [];
}