namespace Core.Model.Interface.ApiErrors;

public interface IApiError
{
	string Code { get; set; }
	object Message { get; set; }
	string Value { get; set; }
}