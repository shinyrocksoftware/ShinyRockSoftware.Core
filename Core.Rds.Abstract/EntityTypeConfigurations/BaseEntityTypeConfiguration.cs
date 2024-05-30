using Base.Model.Interface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Rds.Abstract.EntityTypeConfigurations;

public abstract class BaseEntityTypeConfiguration<T, TE> : BasePlainEntityTypeConfiguration<T, TE>
	where TE : class, IEntity<T>
{
	public override void Configure(EntityTypeBuilder<TE> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.IsActive)
		       .IsRequired()
		       .HasDefaultValue(true);

		builder.Property(e => e.CreatedAt)
		       .ValueGeneratedOnAdd()
		       .HasDefaultValue(DateTime.UtcNow);

		builder.Property(e => e.LastModifiedAt)
		       .ValueGeneratedOnUpdate()
		       .HasDefaultValue(DateTime.UtcNow);
	}
}