using Client.iTaxViet.CompanyService.v1.Stream.StreamJobs;
using Client.iTaxViet.CompanyService.v1.Stream.StreamJobs.Interfaces;
using Core.Attribute.AutoInjection;
using Base.Model.Interface;
using Microsoft.Extensions.Logging;

namespace Client.iTaxViet.CompanyService.v1.App.Background.Stream.StreamJobs;

[ScopedAutoInjection]
public class CompanyDeletedStreamJob(ILogger<CompanyDeletedStreamJob> logger)
	: BaseCompanyDeletedStreamJob(logger), ICompanyDeletedStreamJob
{
	public override string GroupId { get; } = CompanyStreamGroups.Deleted;

	public override Func<IStreamMessage, Task> ConsumeDelegate { get; } = async message =>
	{
		await Task.CompletedTask;
	};
}