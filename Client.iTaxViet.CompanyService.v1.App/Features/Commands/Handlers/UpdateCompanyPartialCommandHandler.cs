using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands.Handlers;

internal class UpdateCompanyPartialCommandHandler(ICompanyService companyService)
	: IRequestHandler<UpdateCompanyPartialCommand, CompanyDto>
{
	public async Task<CompanyDto> Handle(UpdateCompanyPartialCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await companyService.UpdateAsync(dbRequest, cancellationToken);
	}
}