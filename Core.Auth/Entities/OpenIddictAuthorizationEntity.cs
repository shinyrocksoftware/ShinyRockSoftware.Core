using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("OpenIddictAuthorizations")]
public class OpenIddictAuthorizationEntity : BasePlainEntity<string>
{
    public string ApplicationId { get; set; }
    public string ConcurrencyToken { get; set; }
    public string Properties { get; set; }
    public string Scopes { get; set; }
    public string Status { get; set; }
    public string Subject { get; set; }
    public string Type { get; set; }
}