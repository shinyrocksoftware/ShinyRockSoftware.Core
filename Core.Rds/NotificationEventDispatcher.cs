using Core.Attribute.AutoInjection;
using Base.Model.Interface;
using Base.Model.Interface.Entities;
using Core.Stream.Interface;
using MediatR;

namespace Core.Rds;

[ScopedAutoInjection]
internal class NotificationEventDispatcher(IPublisher mediator, IStreamHelper streamHelper) : INotificationEventDispatcher
{
    public async Task DispatchAndClearEventsAsync<T>(IEnumerable<IPlainEntity<T>> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var notificationEvents = entity.NotificationEvents.ToArray();

            entity.ClearNotificationEvents();

            foreach (var notificationEvent in notificationEvents)
            {
                notificationEvent.App = streamHelper.App;
                notificationEvent.Version = streamHelper.Version;

                await mediator.Publish(notificationEvent).ConfigureAwait(false);

                notificationEvent.TopicName = streamHelper.LifetimeTrackingTopic;

                await mediator.Publish(notificationEvent).ConfigureAwait(false);
            }
        }
    }
}