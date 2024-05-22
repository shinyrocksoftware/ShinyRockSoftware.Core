using Core.Model.Interface;

namespace Core.Stream.Interface;

public interface IStreamJob
{
	public string GroupId { get; }
	public string Topic { get; }

	public Task ConsumeAsync(IStreamMessage streamMessage, CancellationToken cancellationToken);
}