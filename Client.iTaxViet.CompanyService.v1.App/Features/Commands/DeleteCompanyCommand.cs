using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands;

public class DeleteCompanyCommand : BaseCommandMediatorRequest<Guid, Company, DeleteCompanyCommandDbRequest>
{
	public DeleteCompanyCommand(Guid id)
	{
		Id = id;
	}
}