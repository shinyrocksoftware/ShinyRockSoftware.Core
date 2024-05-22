using Core.Model.Interface;
using MediatR;
using Shared.FileService.v1.App.Services.Interfaces;

namespace Shared.FileService.v1.App.Features.Queries.Handlers;

internal class GetPagedFilesCommandHandler(IFileService fileService)
	:  IRequestHandler<GetPagedFilesQuery, IEnumerablePage<FileDto>>
{
	public async Task<IEnumerablePage<FileDto>> Handle(GetPagedFilesQuery request, CancellationToken cancellationToken)
	{
		var dbRequest = request.ToDbRequest();
		return await fileService.GetPagedAsync(dbRequest, cancellationToken);
	}
}