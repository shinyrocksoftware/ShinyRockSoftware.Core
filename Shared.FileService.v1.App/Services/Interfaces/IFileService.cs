using Core.Model.Interface;
using Shared.FileService.v1.App.DbRequests;

namespace Shared.FileService.v1.App.Services.Interfaces;

public interface IFileService : IAutoInjection
{
	ValueTask<IEnumerablePage<FileDto>> GetPagedAsync(GetPagedFilesQueryDbRequest request, CancellationToken cancellationToken);
	ValueTask<FileDto?> GetByIdAsync(GetFileByIdQueryDbRequest request, CancellationToken cancellationToken);
	ValueTask<FileDto> CreateAsync(CreateFileCommandDbRequest request, CancellationToken cancellationToken);
	ValueTask<bool> DeleteByIdAsync(DeleteFileCommandDbRequest request, CancellationToken cancellationToken);
}