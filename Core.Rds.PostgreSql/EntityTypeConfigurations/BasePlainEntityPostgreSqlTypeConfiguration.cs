using Core.Model.Interface.Entities;
using Core.Rds.Abstract.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Rds.PostgreSql.EntityTypeConfigurations;

public abstract class BasePlainEntityPostgreSqlTypeConfiguration<T, TE> : BasePlainEntityTypeConfiguration<T, TE>
	where TE : class, IPlainEntity<T>
{
	public override void Configure(EntityTypeBuilder<TE> builder)
	{
		base.Configure(builder);

		var converter = new ValueConverter<byte[], long>(
			v => BitConverter.ToInt64(v, 0),
			v => BitConverter.GetBytes(v));

		builder.Property(e => e.RowVersion)
		       .HasColumnName("xmin")
		       .HasColumnType("xid")
		       .HasConversion(converter);
	}
}