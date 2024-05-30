using Core.Model.Abstract.Entities;
using Base.Model.Interface.Entities;

namespace Shared.LifetimeTrackingService.v1;

public class EntityLifetimeDto : BasePlainEntityDto<Guid>, IPlainEntityDto<Guid>
{
	public string App { get; set; }
	public string Version { get; set; }
	public Guid EntityId { get; set; }
	public string Content { get; set; }
	public string ChangedType { get; set; }
	public string ChangedBy { get; set; }
	public DateTime ChangedAt { get; set; }
}