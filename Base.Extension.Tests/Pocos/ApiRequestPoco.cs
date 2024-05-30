using System.Text.Json.Serialization;

namespace Base.Extension.Tests.Pocos;

public class ApiRequestPoco
{
	public string Name { get; set; }

	[JsonPropertyName("nick")]
	public string NickName { get; set; }

	public DateTime Dob { get; set; }
	public int Height { get; set; }

	public IList<string> Friends { get; set; }
	public IDictionary<string, string> Extensions { get; set; }
}