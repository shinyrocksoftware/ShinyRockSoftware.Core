using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Interface;

namespace Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;

public interface ICompanyService : IAutoInjection
{
	ValueTask<IEnumerablePage<CompanyDto>> GetPagedAsync(GetPagedCompaniesQueryDbRequest request, CancellationToken cancellationToken);
	ValueTask<CompanyDto?> GetByIdAsync(GetCompanyByIdQueryDbRequest request, CancellationToken cancellationToken);
	ValueTask<CompanyDto> CreateAsync(CreateCompanyCommandDbRequest request, CancellationToken cancellationToken);
	ValueTask<CompanyDto> UpdateAsync(UpdateCompanyCommandDbRequest request, CancellationToken cancellationToken);
	ValueTask<CompanyDto> UpdateAsync(UpdateCompanyPartialCommandDbRequest request, CancellationToken cancellationToken);
	ValueTask<bool> DeleteByIdAsync(DeleteCompanyCommandDbRequest request, CancellationToken cancellationToken);
}