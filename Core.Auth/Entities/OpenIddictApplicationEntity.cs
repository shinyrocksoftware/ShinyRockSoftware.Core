using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("OpenIddictApplications")]
public class OpenIddictApplicationEntity : BasePlainEntity<string>
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string DisplayName { get; set; }
    public string Permissions { get; set; }
    public string Properties { get; set; }
    public string PostLogoutRedirectUris { get; set; }
    public string RedirectUris { get; set; }
    public string Type { get; set; }
}