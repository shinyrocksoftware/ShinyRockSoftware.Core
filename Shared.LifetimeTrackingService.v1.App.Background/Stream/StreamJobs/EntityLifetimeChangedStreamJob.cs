using Core.Attribute.AutoInjection;
using Core.Extension;
using Core.Model.Abstract.NotificationEvents;
using Core.Model.Interface;
using Core.Stream.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.LifetimeTrackingService.v1.App.Background.Stream.StreamJobs.Interfaces;
using Shared.LifetimeTrackingService.v1.App.Features.Commands;

namespace Shared.LifetimeTrackingService.v1.App.Background.Stream.StreamJobs;

[ScopedAutoInjection]
internal class EntityLifetimeChangedStreamJob(IStreamHelper streamHelper, ILogger<EntityLifetimeChangedStreamJob> logger, IMediator mediator)
	: IEntityLifetimeChangedStreamJob
{
	public string GroupId => EntityLifetimeStreamGroups.Changed;
	public string Topic => streamHelper.LifetimeTrackingTopic;

	public async Task ConsumeAsync(IStreamMessage streamMessage, CancellationToken cancellationToken)
	{
		logger.LogInformation("Detected changes in entity object : {Value}", streamMessage.Value);

		var notificationEvent = await streamMessage.Value.DeserializeAsync<BaseNotificationEvent>();

		if (notificationEvent == null)
		{
			logger.LogError("Consuming error {Value}", streamMessage.Value);
			return;
		}

		var entityId = await notificationEvent.Entity.ToString().DeserializeAsync<Core.Model.Entity<Guid>>();
		if (entityId == null)
		{
			logger.LogError("Consuming error {Value}", streamMessage.Value);
			return;
		}

		var command = new ChangeEntityLifetimeCommand
		{
			App = notificationEvent.App
			, Version = notificationEvent.Version
			, EntityId = entityId.Id
			, Content = notificationEvent.Entity.Serialize()
			, ChangedBy = "System"
			, ChangedType = notificationEvent.ActionType
			, ChangedAt = notificationEvent.DateOccurred
		};

		await mediator.Send(command, cancellationToken);
	}
}