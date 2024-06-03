using Base.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IWriteRepository<T, TE> : IPlainWriteRepository<T, TE>
	where TE : IEntity<T>
{
	new ValueTask BulkInsertAsync(IEnumerable<TE> entities, CancellationToken cancellationToken);

	new ValueTask<TE> AddAsync(TE entity, CancellationToken cancellationToken);

	new ValueTask<TE> UpdateByIdAsync(T id, TE entity, CancellationToken cancellationToken);

	new ValueTask DeleteByIdAsync(T id, TE entity, CancellationToken cancellationToken);

	new ValueTask<int> SaveAsync(CancellationToken cancellationToken);
}