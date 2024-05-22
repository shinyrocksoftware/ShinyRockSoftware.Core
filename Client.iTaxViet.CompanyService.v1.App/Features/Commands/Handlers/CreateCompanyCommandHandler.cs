using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands.Handlers;

internal class CreateCompanyCommandHandler(ICompanyService companyService)
	: IRequestHandler<CreateCompanyCommand, CompanyDto>
{
	public async Task<CompanyDto> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await companyService.CreateAsync(dbRequest, cancellationToken);
	}
}