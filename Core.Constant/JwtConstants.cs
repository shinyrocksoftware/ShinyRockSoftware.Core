using System.Security.Cryptography;

namespace Core.Constant;

public static class JwtConstants
{
	public static IDictionary<ECCurve, string> AsymmetricCurves = new Dictionary<ECCurve, string>
	{
		{ ECCurve.NamedCurves.nistP256, "ES256" }
		, { ECCurve.NamedCurves.nistP384, "ES384" }
		, { ECCurve.NamedCurves.nistP521, "ES512" }
	};
		
	public static class AsymmetricAlgorithm
	{
		public const string ES256 = "ES256";
		public const string ES384 = "ES384";
		public const string ES512 = "ES512";
	}

	public static class SymmetricAlgorithm
	{
		public const string HS256 = "HS256";
		public const string HS384 = "HS384";
		public const string HS512 = "HS512";
	}
}