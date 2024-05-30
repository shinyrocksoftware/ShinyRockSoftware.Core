using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using Base.Model.Interface;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Queries.Handlers;

internal class GetPagedCompaniesCommandHandler(ICompanyService companyService)
	:  IRequestHandler<GetPagedCompaniesQuery, IEnumerablePage<CompanyDto>>
{
	public async Task<IEnumerablePage<CompanyDto>> Handle(GetPagedCompaniesQuery request, CancellationToken cancellationToken)
	{
		var dbRequest = request.ToDbRequest();
		return await companyService.GetPagedAsync(dbRequest, cancellationToken);
	}
}