using Core.Model.Abstract.MediatorRequests;
using Core.Model.Interface.MediatorRequests;
using Shared.LifetimeTrackingService.v1.App.DbRequests;

namespace Shared.LifetimeTrackingService.v1.App.Features.Commands;

public class ChangeEntityLifetimeCommand : BasePlainCommandMediatorRequest<Guid, EntityLifetime, EntityLifetimeDto, ChangeEntityLifetimeCommandDbRequest>
                                           , IPlainCommandMediatorRequest<Guid, EntityLifetime, EntityLifetimeDto, ChangeEntityLifetimeCommandDbRequest>
{
	public string App { get; set; }
	public string Version { get; set; }
	public Guid EntityId { get; set; }
	public string Content { get; set; }
	public string ChangedType { get; set; }
	public string ChangedBy { get; set; }
	public DateTime ChangedAt { get; set; }
}