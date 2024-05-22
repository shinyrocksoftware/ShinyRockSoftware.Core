using Client.iTaxViet.CompanyService.v1.App.Services.Interfaces;
using MediatR;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Queries.Handlers;

internal class GetCompanyByIdCommandHandler(ICompanyService companyService)
	:  IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
	public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
	{
		var dbRequest = request.ToDbRequest();
		return await companyService.GetByIdAsync(dbRequest, cancellationToken);
	}
}