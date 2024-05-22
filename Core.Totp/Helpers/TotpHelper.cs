using Core.Totp.Helpers.Interfaces;
using OtpNet;

namespace Core.Totp.Helpers;

public class TotpHelper : ITotpHelper
{
	public string GenerateSharedSecret()
	{
		var secret = KeyGeneration.GenerateRandomKey(20);
		return Base32Encoding.ToString(secret);
	}

	public string GenerateTotp(string secret, int size = 6)
	{
		var totp = new OtpNet.Totp(Encoding.UTF8.GetBytes(secret), 30, OtpHashMode.Sha512, size);
		return totp.ComputeTotp();
	}

	public static string Generate(string uniqueDataString)
	{
		var totpObj = new OtpNet.Totp(Encoding.UTF8.GetBytes(uniqueDataString), 30, OtpHashMode.Sha512, 8, new (DateTime.Today));
		return totpObj.ComputeTotp();
	}

	public static bool TryVerify(string uniqueDataString, string totp, out string errorMessage)
	{
		errorMessage = string.Empty;

		var totpObj = new OtpNet.Totp(Encoding.UTF8.GetBytes(uniqueDataString), 30, OtpHashMode.Sha512, 8, new (DateTime.Today));
		if (totpObj.VerifyTotp(totp, out var timeWindowUsed, VerificationWindow.RfcSpecifiedNetworkDelay))
		{
			if (timeWindowUsed <= 1)
				return true;

			errorMessage = "The OTP is used before";
			return false;
		}

		errorMessage = "The OTP is invalid";
		return false;
	}
}