using Core.Model.Abstract.MediatorRequests;
using Shared.FileService.v1.App.DbRequests;

namespace Shared.FileService.v1.App.Features.Queries;

public class GetFileByIdQuery(Guid id)
	: BaseQueryMediatorRequest<Guid, FileDto, GetFileByIdQueryDbRequest>(id);