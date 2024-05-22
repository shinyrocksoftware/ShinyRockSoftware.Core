using System.ComponentModel.DataAnnotations;
using Core.Model.Abstract.Entities;
using Core.Model.Interface.Entities;

namespace Shared.FileService.v1;

public partial class File : BaseEntity<Guid>, IEntity<Guid>
{
	[Required, MaxLength(1024)]
	public string Name { get; set; }

	[Required]
	public float Size { get; set; }

	[Required]
	public Guid ProviderId { get; set; }

	[MaxLength(2048)]
	public string? Location { get; set; }

	[MaxLength(255)]
	public string? Extension { get; set; }

	public File? Parent { get; set; }
	public Guid? ParentId { get; set; }

	public virtual ICollection<File> Versions { get; set; }
}