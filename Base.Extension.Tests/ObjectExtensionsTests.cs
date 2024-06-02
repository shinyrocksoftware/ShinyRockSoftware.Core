using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Base.Extension.Tests;

public class ObjectExtensionsTests
{
	[TestCase]
	public void As_ObjectIntValue_ReturnsIntValue()
	{
		object str = 123;

		var value = str.As<int>();

		 ClassicAssert.IsInstanceOf<int>(value);
	}

	[TestCase]
	public void ToByteArray_AnyObject_ReturnsByteArray()
	{
		object str = 123;

		var value = str.ToByteArray();

		 ClassicAssert.IsInstanceOf<byte[]>(value);
	}
}