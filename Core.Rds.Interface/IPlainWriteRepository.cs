﻿using Base.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IPlainWriteRepository<T, TE>
	where TE : IPlainEntity<T>
{
	ValueTask BulkInsertAsync(IEnumerable<TE> entities, CancellationToken cancellationToken);

	ValueTask<TE> AddAsync(TE entity, CancellationToken cancellationToken);

	ValueTask<TE> UpdateByIdAsync(T id, TE entity, CancellationToken cancellationToken);

	ValueTask DeleteByIdAsync(T id, TE entity, CancellationToken cancellationToken);

	ValueTask<int> SaveAsync(CancellationToken cancellationToken);
}