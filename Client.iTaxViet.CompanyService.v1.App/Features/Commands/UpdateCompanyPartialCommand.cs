using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;
using Core.Model.Interface.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Commands;

public class UpdateCompanyPartialCommand : BaseCommandMediatorRequest<Guid, Company, CompanyDto, UpdateCompanyPartialCommandDbRequest>
                                           , ICommandMediatorRequest<Guid, Company, CompanyDto, UpdateCompanyPartialCommandDbRequest>
{
	public string? Name { get; set; }
	public string? Code { get; set; }
	public string? TaxIdentificationNumber { get; set; }
	public DateTime? EstablishmentAt { get; set; }
}