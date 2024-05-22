using Core.Model.Interface.Entities;

namespace Core.Rds.PostgreSql.EntityTypeConfigurations;

public abstract class BaseEntityPostgreSqlTypeConfiguration<T, TE> : BasePlainEntityPostgreSqlTypeConfiguration<T, TE>
	where TE : class, IEntity<T>;