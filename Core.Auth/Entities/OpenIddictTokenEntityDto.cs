using Core.Model.Abstract.Entities;

namespace Core.Auth.Entities;

public class OpenIddictTokenEntityDto : BasePlainEntityDto<Guid>
{
    public string Subject { get; set; }
}