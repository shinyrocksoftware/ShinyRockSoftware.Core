using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;
using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Auth.Entities;

[Keyless]
[Table("AspNetUserLogins")]
public class AspNetUserLogin : BasePlainEntity<Guid>, IPlainEntity<Guid>
{
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
    public string ProviderDisplayName { get; set; }
    public Guid UserId { get; set; }
}