namespace Core.Totp.Helpers.Interfaces;

public interface ITotpHelper
{
	string GenerateSharedSecret();
}