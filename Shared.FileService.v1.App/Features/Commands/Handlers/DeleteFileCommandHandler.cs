using MediatR;
using Shared.FileService.v1.App.Services.Interfaces;

namespace Shared.FileService.v1.App.Features.Commands.Handlers;

internal class DeleteFileCommandHandler(IFileService fileService)
	: IRequestHandler<DeleteFileCommand, bool>
{
	public async Task<bool> Handle(DeleteFileCommand command, CancellationToken cancellationToken)
	{
		var dbRequest = command.ToDbRequest();
		return await fileService.DeleteByIdAsync(dbRequest, cancellationToken);
	}
}