using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("AspNetRoles")]
public class AspNetRole : BasePlainEntity<Guid>
{
    public string ConcurrencyStamp { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public IEnumerable<AspNetUserRole> UserRoleDetails { get; } = Enumerable.Empty<AspNetUserRole>();
}