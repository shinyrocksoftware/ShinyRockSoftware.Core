namespace Core.Model.Interface;

public interface IStreamMessage
{
	string Topic { get; set; }
	string Value { get; set; }
	DateTime Timestamp { get; set; }
}