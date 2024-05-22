using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands.Handlers;

internal class DeleteCompanyCommandHandler(ICompanyService companyService)
	: IRequestHandler<DeleteCompanyCommand, bool>
{
	public async Task<bool> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await companyService.DeleteByIdAsync(dbRequest, cancellationToken);
	}
}