using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;
using Core.Model.Interface.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands;

public class CreateCompanyCommand : BaseCommandMediatorRequest<Guid, Company, CompanyDto, CreateCompanyCommandDbRequest>
                                    , ICommandMediatorRequest<Guid, Company, CompanyDto, CreateCompanyCommandDbRequest>
{
	public string Name { get; set; }
	public string Code { get; set; }
	public string TaxIdentificationNumber { get; set; }
	public DateTime EstablishmentAt { get; set; }
}