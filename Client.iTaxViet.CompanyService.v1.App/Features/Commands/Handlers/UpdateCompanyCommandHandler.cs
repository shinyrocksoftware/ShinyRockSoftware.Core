using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands.Handlers;

internal class UpdateCompanyCommandHandler(ICompanyService companyService)
	: IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
	public async Task<CompanyDto> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await companyService.UpdateAsync(dbRequest, cancellationToken);
	}
}