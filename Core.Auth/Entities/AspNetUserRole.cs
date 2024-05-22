using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("AspNetUserRoles")]
public class AspNetUserRole : BasePlainEntity<Guid>
{
    public string UserId { get; set; }
    public AspNetUser User { get; set; }
    public string RoleId { get; set; }
    public AspNetRole AspNetRole { get; set; }
}