using Core.Totp.Helpers;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OtpNet;

namespace Core.Totp.Tests;

public class TotpHelperTests
{
	[Test]
	public void Generate_Totp()
	{
		var totp = TotpHelper.Generate("Key");
		ClassicAssert.IsNotNull(totp);
	}

	[Test]
	public void Generate_Totp_And_Verify()
	{
		var totp = Generate();
		Thread.Sleep(5000);
		var result = Verify(totp);

		ClassicAssert.IsTrue(result);
	}

	private string Generate()
	{
		var totpObj = new OtpNet.Totp(Encoding.UTF8.GetBytes("Key"), totpSize: 6);
		var totp = totpObj.ComputeTotp();
		return totp;
	}

	private bool Verify(string totp)
	{
		var totpObj2 = new OtpNet.Totp(Encoding.UTF8.GetBytes("Key"), totpSize: 6);
		return totpObj2.VerifyTotp(totp, out var timeWindowUsed, VerificationWindow.RfcSpecifiedNetworkDelay);
	}
}