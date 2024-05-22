namespace Shared.LifetimeTrackingService.v1.App.Background.Stream;

public class EntityLifetimeStreamGroups
{
	public static string Changed => $"{typeof(EntityLifetime)}.Changed";
}