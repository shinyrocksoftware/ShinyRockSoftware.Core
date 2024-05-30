using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Rds.Abstract.EntityTypeConfigurations;

public abstract class BasePlainEntityTypeConfiguration<T, TE>
	where TE : class, IPlainEntity<T>
{
	public virtual void Configure(EntityTypeBuilder<TE> builder)
	{
		builder.HasKey(e => e.Id);
	}
}