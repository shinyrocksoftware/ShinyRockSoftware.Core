using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Queries;

public class GetPagedCompaniesQuery(int pageNumber, int pageSize)
	: BasePagedQueryMediatorRequest<Guid, CompanyDto, GetPagedCompaniesQueryDbRequest>(pageNumber, pageSize);