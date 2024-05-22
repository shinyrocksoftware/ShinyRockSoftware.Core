namespace Core.File.Interface;

public interface IFileFunction
{
	Stream Download(string fileUrl);
}