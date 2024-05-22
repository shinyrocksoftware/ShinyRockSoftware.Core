namespace Client.iTaxViet.CompanyService.v1.App.Background.Stream;

public class CompanyStreamGroups
{
	public static string Created => $"{typeof(Company)}.Created";
	public static string Updated => $"{typeof(Company)}.Updated";
	public static string Deleted => $"{typeof(Company)}.Deleted";
}