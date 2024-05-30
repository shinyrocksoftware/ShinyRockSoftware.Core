using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;
using Base.Model.Interface.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands;

public class UpdateCompanyCommand : BaseCommandMediatorRequest<Guid, Company, CompanyDto, UpdateCompanyCommandDbRequest>
                                    , ICommandMediatorRequest<Guid, Company, CompanyDto, UpdateCompanyCommandDbRequest>
{
	public string Name { get; set; }
	public string Code { get; set; }
	public string TaxIdentificationNumber { get; set; }
	public DateTime EstablishmentAt { get; set; }
}