using Client.iTaxViet.CompanyService.v1.Stream.StreamJobs;
using Client.iTaxViet.CompanyService.v1.Stream.StreamJobs.Interfaces;
using Core.Attribute.AutoInjection;
using Core.Model.Interface;
using Microsoft.Extensions.Logging;

namespace Client.iTaxViet.CompanyService.v1.App.Background.Stream.StreamJobs;

[ScopedAutoInjection]
public class CompanyUpdatedStreamJob(ILogger<CompanyUpdatedStreamJob> logger)
	: BaseCompanyUpdatedStreamJob(logger), ICompanyUpdatedStreamJob
{
	public override string GroupId { get; } = CompanyStreamGroups.Updated;

	public override Func<IStreamMessage, Task> ConsumeDelegate { get; } = async message =>
	{
		await Task.CompletedTask;
	};
}