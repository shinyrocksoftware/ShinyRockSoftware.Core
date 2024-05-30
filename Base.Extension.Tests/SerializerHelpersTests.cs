using Base.Extension;
using Base.Extension.Tests.Pocos;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Base.Extension.Tests;

public class SerializerHelperTests
{
	[SetUp]
	public void SetUp()
	{
	}

	[Test]
	public void Serialize_Poco_ReturnsJsonString()
	{
		var json = new ApiRequestSmallPoco
		{
			Name = "ABC"
		};

		var serializedJson = json.Serialize();

		Console.WriteLine(serializedJson);

		 ClassicAssert.IsTrue(serializedJson.Equals("{\"Name\":\"ABC\"}"));
	}

	[Test]
	public void Deserialize_JsonString_ReturnsPoco()
	{
		var jsonString = "{\"Name\":\"ABC\"}";

		var json = jsonString.Deserialize<ApiRequestSmallPoco>();

		 ClassicAssert.AreEqual(json.Name, "ABC");
	}

	[Test]
	public void DeserializeNonGeneric_JsonString_ReturnsPoco()
	{
		var jsonString = "{\"Name\":\"ABC\"}";

		var json = jsonString.Deserialize();

		 ClassicAssert.AreEqual(json?.GetProperty("Name").GetString(), "ABC");
	}

	[Test]
	public void DeserializeDynamic_InvalidJsonString_ReturnsNull()
	{
		var invalidJsonString = "{Name:ABC}";

		var json = invalidJsonString.Deserialize();

		 ClassicAssert.IsNull(json);
	}

	[Test]
	public async Task DeserializeAsync_JsonString_ReturnsPoco()
	{
		var jsonString = "{\"Name\":\"ABC\"}";

		var json = await jsonString.DeserializeAsync<ApiRequestSmallPoco>();

		 ClassicAssert.AreEqual(json.Name, "ABC");
	}

	[Test]
	public async Task DeserializeAsyncNonGeneric_JsonString_ReturnsPoco()
	{
		var invalidJsonString = "{Name:ABC}";

		var json = await invalidJsonString.DeserializeAsync();

		 ClassicAssert.IsNull(json);
	}
}