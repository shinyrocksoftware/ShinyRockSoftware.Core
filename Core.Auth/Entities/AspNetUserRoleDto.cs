using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

public class AspNetUserRoleDto : BasePlainEntityDto<Guid>
{
    public string UserId { get; set; }
    public AspNetUserDto User { get; set; }
    public string RoleId { get; set; }
    public AspNetRoleDto AspNetRole { get; set; }
}