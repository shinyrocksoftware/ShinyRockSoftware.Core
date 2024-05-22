using Core.Rds.PostgreSql.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.LifetimeTrackingService.v1.App.TypeConfigurations;

public class EntityLifetimeTypeConfiguration : BasePlainEntityPostgreSqlTypeConfiguration<Guid, EntityLifetime>
                                               , IEntityTypeConfiguration<EntityLifetime>
{
	public override void Configure(EntityTypeBuilder<EntityLifetime> builder)
	{
		base.Configure(builder);

		builder.ToTable("entityLifetime");
	}
}