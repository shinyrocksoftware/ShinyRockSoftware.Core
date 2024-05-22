using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("OpenIddictTokens")]
public class OpenIddictTokenEntity : BasePlainEntity<Guid>
{
    public string Subject { get; set; }
}