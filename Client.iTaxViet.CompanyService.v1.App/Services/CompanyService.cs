using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using Core.Attribute.AutoInjection;
using Core.Model.Interface;
using Core.ObjectMapper.Extensions;
using Core.Rds.Interface;

namespace Client.iTaxViet.CompanyService.v1.App.Services;

[ScopedAutoInjection]
internal class CompanyService(IReadRepository<Guid, Company, CompanyDto> readRepository, IWriteRepository<Guid, Company> writeRepository)
	: ICompanyService
{
	public async ValueTask<IEnumerablePage<CompanyDto>> GetPagedAsync(GetPagedCompaniesQueryDbRequest request, CancellationToken cancellationToken)
	{
		return await readRepository.GetPagedAsync(request.PageNumber, request.PageSize, null, null, null, null, cancellationToken);
	}

	public async ValueTask<CompanyDto?> GetByIdAsync(GetCompanyByIdQueryDbRequest request, CancellationToken cancellationToken)
	{
		return await readRepository.GetByIdAsync(request.Id, null, cancellationToken);
	}

	public async ValueTask<CompanyDto> CreateAsync(CreateCompanyCommandDbRequest request, CancellationToken cancellationToken)
	{
		var company = await writeRepository.AddAsync(request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return company.To<CompanyDto>();
	}

	public async ValueTask<CompanyDto> UpdateAsync(UpdateCompanyCommandDbRequest request, CancellationToken cancellationToken)
	{
		var company = await writeRepository.UpdateByIdAsync(request.Id, request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return company.To<CompanyDto>();
	}

	public async ValueTask<CompanyDto> UpdateAsync(UpdateCompanyPartialCommandDbRequest request, CancellationToken cancellationToken)
	{
		var company = await writeRepository.UpdateByIdAsync(request.Id, request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return company.To<CompanyDto>();
	}

	public async ValueTask<bool> DeleteByIdAsync(DeleteCompanyCommandDbRequest request, CancellationToken cancellationToken)
	{
		await writeRepository.DeleteByIdAsync(request.Id, request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return true;
	}
}