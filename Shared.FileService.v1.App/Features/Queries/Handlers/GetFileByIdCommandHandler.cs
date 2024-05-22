using MediatR;
using Shared.FileService.v1.App.Services.Interfaces;

namespace Shared.FileService.v1.App.Features.Queries.Handlers;

internal class GetFileByIdCommandHandler(IFileService fileService)
	:  IRequestHandler<GetFileByIdQuery, FileDto?>
{
	public async Task<FileDto?> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
	{
		var dbRequest = request.ToDbRequest();
		return await fileService.GetByIdAsync(dbRequest, cancellationToken);
	}
}