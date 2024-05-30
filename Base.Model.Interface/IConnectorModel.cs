namespace Base.Model.Interface;

public interface IConnectorModel
{
	bool IsValid { get; }
	bool IsDataInvalid { get; }
	string? InvalidMessage { get; }
	public bool PreValidationOverriden { get; set; }

	bool ReturnWithError(string? invalidMessage);
	/// <summary>
	/// In case the InvalidMessage needs to be dynamic bases on the object state
	/// </summary>
	/// <param name="invalidMessage"></param>
	void SetInvalidMessage(string invalidMessage);
}