using Base.Model.Interface;

namespace Core.Stream.Interface;

public interface IStreamHelper : IAutoInjection
{
	string LifetimeTrackingTopic { get; }
	string App { get; }
	string Version { get; }
}