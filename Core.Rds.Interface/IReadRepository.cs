using Core.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IReadRepository<T, TE, TEO> : IPlainReadRepository<T, TE, TEO>
	where TE : IEntity<T>
	where TEO : IEntityDto<T>, new();