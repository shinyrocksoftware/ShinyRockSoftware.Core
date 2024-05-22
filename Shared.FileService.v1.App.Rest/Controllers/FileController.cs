using App.Rest.Abstract.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.FileService.v1.App.Features.Commands;
using Shared.FileService.v1.App.Features.Queries;

namespace Shared.FileService.v1.App.Rest.Controllers;

[Route("files")]
public class FileController(ILogger<FileController> logger, IMediator mediator)
	: BaseApiController(logger)
{
	[HttpGet("{pageSize:int}/{pageNumber:int}/{rootId:guid}")]
	public async Task<IActionResult> GetPaged(Guid? rootId, int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		var entities = await mediator.Send(new GetPagedFilesQuery(rootId, pageNumber, pageSize), cancellationToken);
		return ReturnSuccess(entities);
	}

	public async Task<IActionResult> GetInfo(Guid id, CancellationToken cancellationToken)
	{
		var entity = await mediator.Send(new GetFileByIdQuery(id), cancellationToken);
		return entity == null ? ReturnNotFound() : ReturnSuccess(entity);
	}

	public Task<IActionResult> Upload<T>(Guid? rootId, T fileNode, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
	{
		var success = await mediator.Send(new DeleteFileCommand(id), cancellationToken);
		return ReturnSuccess(success);
	}
}