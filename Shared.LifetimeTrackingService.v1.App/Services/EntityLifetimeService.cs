using Core.Attribute.AutoInjection;
using Core.ObjectMapper.Extensions;
using Core.Rds.Interface;
using Shared.LifetimeTrackingService.v1.App.DbRequests;
using Shared.LifetimeTrackingService.v1.App.Services.Interfaces;

namespace Shared.LifetimeTrackingService.v1.App.Services;

[ScopedAutoInjection]
internal class EntityLifetimeService(IPlainReadRepository<Guid, EntityLifetime, EntityLifetimeDto> readRepository, IPlainWriteRepository<Guid, EntityLifetime> writeRepository)
	: IEntityLifetimeService
{
	public async ValueTask<EntityLifetimeDto> ChangeAsync(ChangeEntityLifetimeCommandDbRequest request, CancellationToken cancellationToken)
	{
		var entity = await writeRepository.AddAsync(request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return entity.To<EntityLifetimeDto>();
	}
}