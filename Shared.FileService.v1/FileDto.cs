using Core.Model.Abstract.Entities;
using Core.Model.Interface.Entities;

namespace Shared.FileService.v1;

public partial class FileDto : BaseEntityDto<Guid>, IEntityDto<Guid>
{
	public string Name { get; set; }
	public float Size { get; set; }
	public string? Location { get; set; }
	public string? Extension { get; set; }
	public Guid? ParentId { get; set; }
}