using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Core.Extension.Tests;

public class StringExtensionsTests
{
	[Test]
	public void HasEmoji_InEmojiStringVersion3_1_ReturnsTrue()
	{
		var str = "Emoji 😀";

		var hasEmoji = str.HasEmoji();

		 ClassicAssert.IsTrue(hasEmoji);
	}

	[Test]
	public void HasEmoji_InNonEmojiStringVersion3_1_ReturnsFalse()
	{
		var str = "No Emoji";

		var hasEmoji = str.HasEmoji();

		 ClassicAssert.IsFalse(hasEmoji);
	}

	[Test]
	public void RemoveEmoji_InEmojiStringVersion3_1_ReturnsNonEmojiString()
	{
		var str = "Emoji 😀";

		var nonEmoji = str.RemoveEmoji();

		 ClassicAssert.AreEqual("Emoji ", nonEmoji);
	}

	[Test]
	public void Encrypt_Decrypt_Without_Key_Match_Result()
	{
		var raw = "123456789";
		var encrypted = raw.Encrypt();
		var decryptedRaw = encrypted.Decrypt();

		 ClassicAssert.AreEqual(raw, decryptedRaw);
	}

	[Test]
	public void Encrypt_Decrypt_With_Key_Match_Result()
	{
		var raw = "123456789";
		var encrypted = raw.Encrypt(out var r);
		var key = r.key;
		var iv = r.iv;
		var decryptedRaw = encrypted.Decrypt(key, iv);

		 ClassicAssert.AreEqual(raw, decryptedRaw);
	}

	[Test]
	public void Pluralize_Text_Returns_Match()
	{
		var singularForm = "Company";

		var pluralized = singularForm.Pluralize();

		 ClassicAssert.AreEqual("Companies", pluralized);
	}
}