using Core.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IWriteRepository<T, TE> : IPlainWriteRepository<T, TE>
	where TE : IEntity<T>
{
	new ValueTask BulkInsertAsync(IEnumerable<TE> entities, CancellationToken cancellationToken);

	new ValueTask<TE> AddAsync(TE entity, CancellationToken cancellationToken);

	new ValueTask<TE> UpdateByIdAsync(object id, TE entity, CancellationToken cancellationToken);

	new ValueTask DeleteByIdAsync(object id, TE entity, CancellationToken cancellationToken);

	new ValueTask<int> SaveAsync(CancellationToken cancellationToken);
}