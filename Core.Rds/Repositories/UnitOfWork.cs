using Core.Attribute.AutoInjection;
using Core.Model.Interface;
using Core.Model.Interface.Entities;
using Core.Rds.DbContexts;
using Core.Rds.Interface;

namespace Core.Rds.Repositories;

[TransientAutoInjection]
internal class UnitOfWork(INotificationEventDispatcher dispatcher, WriteDbContext dbContext)
	: IUnitOfWork
{
	private readonly WriteDbContext _dataContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	private Hashtable? _repositories;
	private bool _disposed;

	public IWriteRepository<T, TE> Repository<T, TE>()
		where TE : class, IEntity<T>
	{
		_repositories ??= new();

		var type = typeof(T).Name;

		if (!_repositories.ContainsKey(type))
		{
			var repositoryInstance = Activator.CreateInstance(typeof(WriteRepository<T, TE>), dispatcher, _dataContext);
			_repositories.Add(type, repositoryInstance);
		}

		return (IWriteRepository<T, TE>) _repositories[type];
	}

	public Task Rollback()
	{
		_dataContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		return Task.CompletedTask;
	}

	public async Task<int> SaveAsync(CancellationToken cancellationToken)
	{
		return await _dataContext.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			if (disposing)
			{
				//dispose managed resources
				_dataContext.Dispose();
			}
		}

		//dispose unmanaged resources
		_disposed = true;
	}
}