using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

[Table("Tokens")]
public class Token : BasePlainEntity<Guid>
{
    public Guid UserId { get; set; }
    public string Service { get; set; }
    public string Purpose { get; set; }
    public string HashedToken { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UsedDate { get; set; }
}