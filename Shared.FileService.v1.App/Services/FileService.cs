using Core.Attribute.AutoInjection;
using Base.Model.Interface;
using Base.ObjectMapper.Extension;
using Core.Rds.Abstract.Extensions;
using Core.Rds.Interface;
using Shared.FileService.v1.App.DbRequests;
using Shared.FileService.v1.App.Services.Interfaces;

namespace Shared.FileService.v1.App.Services;

[ScopedAutoInjection]
internal class FileService(IReadRepository<Guid, File, FileDto> readRepository, IWriteRepository<Guid, File> writeRepository)
	: IFileService
{
	public async ValueTask<IEnumerablePage<FileDto>> GetPagedAsync(GetPagedFilesQueryDbRequest request, CancellationToken cancellationToken)
	{
		return await readRepository.Query()
		                           .SelectPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
	}

	public async ValueTask<FileDto?> GetByIdAsync(GetFileByIdQueryDbRequest request, CancellationToken cancellationToken)
	{
		return await readRepository.Query().SelectByIdAsync(request.Id, cancellationToken);
	}

	public async ValueTask<FileDto> CreateAsync(CreateFileCommandDbRequest request, CancellationToken cancellationToken)
	{
		var company = await writeRepository.AddAsync(request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return company.To<FileDto>();
	}

	public async ValueTask<bool> DeleteByIdAsync(DeleteFileCommandDbRequest request, CancellationToken cancellationToken)
	{
		await writeRepository.DeleteByIdAsync(request.Id, request.ToEntity(), cancellationToken);
		await writeRepository.SaveAsync(cancellationToken);

		return true;
	}
}