using Core.Model.Interface;
using Core.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IUnitOfWork : IDisposable, IAutoInjection
{
	IWriteRepository<T, TE> Repository<T, TE>()
		where TE : class, IEntity<T>;
	Task<int> SaveAsync(CancellationToken cancellationToken);
	Task Rollback();
}
