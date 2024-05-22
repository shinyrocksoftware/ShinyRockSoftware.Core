using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

public class OpenIddictAuthorizationEntityDto : BasePlainEntityDto<Guid>
{
    public string ApplicationId { get; set; }
    public string ConcurrencyToken { get; set; }
    public string Properties { get; set; }
    public string Scopes { get; set; }
    public string Status { get; set; }
    public string Subject { get; set; }
    public string Type { get; set; }
}