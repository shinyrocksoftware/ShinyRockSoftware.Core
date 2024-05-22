namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Models;

public class EntityLifeCycleConstants
{
	public static readonly string[] Actions =
	[
		"Create"
		, "Update"
		, "UpdatePartial"
		, "Delete"
	];

	public static readonly string[] Names =
	[
		"Created"
		, "Updated"
		, "Deleted"
	];
}