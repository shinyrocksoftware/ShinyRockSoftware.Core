using Core.BackgroundService.Abstract;
using Core.Stream.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Stream.Abstract.BackgroundServices;

public abstract class BaseStreamBackgroundService : BasePeriodicBackgroundService
{
	private readonly IStreamConsumer _streamConsumer;
	private readonly IStreamJob _streamJob;

	protected BaseStreamBackgroundService(IServiceScopeFactory serviceScopeFactory, IStreamJob streamJob)
		: base(serviceScopeFactory)
	{
		using var asyncScope = ServiceScopeFactory.CreateScope();
		_streamConsumer = asyncScope.ServiceProvider.GetRequiredService<IStreamConsumer>();
		_streamJob = streamJob;
	}

	public override async Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken)
	{
		await _streamConsumer.ConsumeAsync(_streamJob.GroupId, _streamJob.Topic, async streamMessage =>
		{
			await _streamJob.ConsumeAsync(streamMessage, stoppingToken);
		});
	}
}