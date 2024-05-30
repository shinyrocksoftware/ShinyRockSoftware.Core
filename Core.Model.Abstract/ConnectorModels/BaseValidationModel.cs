using System.Text.Json.Serialization;
using Base.Extension;

namespace Core.Model.Abstract.ConnectorModels;

public abstract class BaseValidationModel
{
    [JsonIgnore]
    public virtual bool IsValid { get; set; } = true;

    [JsonIgnore]
    public bool IsDataInvalid { get; set; }

    [JsonIgnore]
    public virtual string? InvalidMessage { get; set; }

    /// <summary>
    /// In case the inheritance object overrides the parent validation
    /// </summary>
    [JsonIgnore]
    public bool PreValidationOverriden { get; set; }

    public bool ReturnWithError(string? invalidMessage)
    {
	    InvalidMessage = invalidMessage;
	    return false;
    }

    /// <summary>
    /// In case the InvalidMessage needs to be dynamic bases on the object state
    /// </summary>
    /// <param name="invalidMessage"></param>
    public void SetInvalidMessage(string invalidMessage)
    {
	    if (invalidMessage.IsNotNullNorEmpty())
	    {
		    IsDataInvalid = true;
		    InvalidMessage = invalidMessage;
	    }
    }
}