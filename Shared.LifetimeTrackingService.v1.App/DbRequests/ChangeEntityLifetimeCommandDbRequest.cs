using Core.Model.Abstract.DbRequests;
using Base.Model.Interface.DbRequests;

namespace Shared.LifetimeTrackingService.v1.App.DbRequests;

public class ChangeEntityLifetimeCommandDbRequest : BasePlainCommandDbRequest<Guid, EntityLifetime>
                                                    , IPlainCommandDbRequest<Guid, EntityLifetime>
{
	public string App { get; set; }
	public string Version { get; set; }
	public Guid EntityId { get; set; }
	public string Content { get; set; }
	public string ChangedType { get; set; }
	public string ChangedBy { get; set; }
	public DateTime ChangedAt { get; set; }
}