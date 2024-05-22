using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("OpenIddictScopes")]
public class OpenIddictScopeEntity : BasePlainEntity<string>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string Properties { get; set; }
    public string Resources { get; set; }
}