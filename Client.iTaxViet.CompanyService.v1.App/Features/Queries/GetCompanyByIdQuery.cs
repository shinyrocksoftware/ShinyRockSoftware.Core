using Client.iTaxViet.CompanyService.v1.App.DbRequests;
using Core.Model.Abstract.MediatorRequests;

namespace Client.iTaxViet.CompanyService.v1.App.Features.Queries;

public class GetCompanyByIdQuery(Guid id)
	: BaseQueryMediatorRequest<Guid, CompanyDto, GetCompanyByIdQueryDbRequest>(id);