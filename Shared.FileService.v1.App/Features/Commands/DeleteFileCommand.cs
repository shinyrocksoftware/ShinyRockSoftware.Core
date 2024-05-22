using Core.Model.Abstract.MediatorRequests;
using Shared.FileService.v1.App.DbRequests;

namespace Shared.FileService.v1.App.Features.Commands;

public class DeleteFileCommand : BaseCommandMediatorRequest<Guid, File, DeleteFileCommandDbRequest>
{
	public DeleteFileCommand(Guid id)
	{
		Id = id;
	}
}