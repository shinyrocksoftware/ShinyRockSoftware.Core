using Core.Model.Abstract.MediatorRequests;
using Shared.FileService.v1.App.DbRequests;

namespace Shared.FileService.v1.App.Features.Queries;

public class GetPagedFilesQuery(Guid? rootId, int pageNumber, int pageSize)
	: BasePagedQueryMediatorRequest<Guid, FileDto, GetPagedFilesQueryDbRequest>(pageNumber, pageSize);