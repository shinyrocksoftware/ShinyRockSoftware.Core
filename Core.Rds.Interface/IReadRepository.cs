using Base.Model.Interface.Entities;

namespace Core.Rds.Interface;

public interface IReadRepository<T, TE, TEO> : IPlainReadRepository<T, TE, TEO>
	where TE : class, IEntity<T>
	where TEO : IEntityDto<T>, new();