using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.FileService.v1.App.TypeConfigurations;

public partial class FileTypeConfiguration
{
	partial void ConfigureAdvanced(EntityTypeBuilder<File> builder)
	{
		builder.HasKey(e => e.Id);

		builder.HasOne(e => e.Parent)
		       .WithMany(e => e.Versions)
		       .HasForeignKey(e => e.ParentId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}