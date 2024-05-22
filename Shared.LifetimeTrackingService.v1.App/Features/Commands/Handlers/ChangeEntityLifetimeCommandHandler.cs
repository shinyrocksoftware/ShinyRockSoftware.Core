using MediatR;
using Shared.LifetimeTrackingService.v1.App.Services.Interfaces;

namespace Shared.LifetimeTrackingService.v1.App.Features.Commands.Handlers;

internal class ChangeEntityLifetimeCommandHandler(IEntityLifetimeService entityLifetimeService)
	: IRequestHandler<ChangeEntityLifetimeCommand, EntityLifetimeDto>
{
	public async Task<EntityLifetimeDto> Handle(ChangeEntityLifetimeCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await entityLifetimeService.ChangeAsync(dbRequest, cancellationToken);
	}
}