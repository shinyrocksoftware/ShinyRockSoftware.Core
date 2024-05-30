using Base.Model.Interface;
using Shared.LifetimeTrackingService.v1.App.DbRequests;

namespace Shared.LifetimeTrackingService.v1.App.Services.Interfaces;

public interface IEntityLifetimeService : IAutoInjection
{
	ValueTask<EntityLifetimeDto> ChangeAsync(ChangeEntityLifetimeCommandDbRequest request, CancellationToken cancellationToken);
}