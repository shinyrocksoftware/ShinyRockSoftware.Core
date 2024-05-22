using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Core.Attribute.AutoInjection;
using Core.Configuration.Interface;
using Core.Jwt.ConnectorModels;
using Core.Jwt.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Core.Jwt;

[SingletonAutoInjection]
internal class JwtHelper(ILogger<JwtHelper> logger, IConnectorModelHelper connectorModelHelper) : IJwtHelper
{
	private readonly ILogger _logger = logger;
	private readonly JwtConnectorModel _connectorModel = connectorModelHelper.GetConnector<JwtConnectorModel>();

	public IDictionary<string, string> SymmetricDecode(string token)
	{
		string stringSecretKey = _connectorModel.SecretKey;
		return SymmetricDecode(token, stringSecretKey);
	}

	public IDictionary<string, string> SymmetricDecode(string token, string secretKey)
	{
		try
		{
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, new()
			{
				ValidateLifetime = false
				, ValidateAudience = false
				, ValidateIssuer = false
				, ValidIssuer = "Sample"
				, ValidAudience = "Sample"
				, IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
			}, out _);
			var cl = claimsPrincipal.Claims;
			return cl.ToDictionary(x => x.Type, x => x.Value);
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "JWT decode failed");
			return new Dictionary<string, string>();
		}
	}

	public string SymmetricEncode(IDictionary<string, object> payload, string algorithm)
	{
		string encodedToken = string.Empty;
		var hsFamily = new List<string>
		{
			"HS256"
			, "HS384"
			, "HS512"
		};

		if (hsFamily.Contains(algorithm))
		{
			string secretKey = _connectorModel.SecretKey;
			encodedToken = SymmetricEncode(payload, secretKey, algorithm);
		}

		return encodedToken;
	}

	public string SymmetricEncode(IDictionary<string, object> payload, string secretKey, string algorithm)
	{
		string encodedToken = string.Empty;

		try
		{
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
			var key = new SymmetricSecurityKey(secretKeyBytes);
			var signingCredentials = new SigningCredentials(key, algorithm);

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Claims = payload
				, Expires = DateTime.UtcNow.AddDays(7)
				, SigningCredentials = signingCredentials
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			encodedToken = tokenHandler.WriteToken(token);
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "JWT encode failed");
		}

		return encodedToken;
	}

	public string AsymmetricEncode(ECDsa privateKey, string algorithm, IDictionary<string, object> claims)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Claims = claims
			, Expires = DateTime.UtcNow.AddDays(7)
			, SigningCredentials = new(new ECDsaSecurityKey(privateKey), algorithm)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	public IDictionary<string, string> AsymmetricDecode(string token, ECDsa publicKey)
	{
		var securityToken = new JwtSecurityToken(token);
		var securityTokenHandler = new JwtSecurityTokenHandler();

		var validationParameters = new TokenValidationParameters
		{
			ValidateLifetime = false
			, ValidateAudience = false
			, ValidateIssuer = false
			, ValidIssuer = securityToken.Issuer
			, ValidAudience = "Sample"
			, IssuerSigningKey = new ECDsaSecurityKey(publicKey)
		};

		var claimsPrincipal = securityTokenHandler.ValidateToken(token, validationParameters, out _);

		return claimsPrincipal.Claims.ToDictionary(x => x.Type, x => x.Value);
	}

	public (ECDsa privateKey, ECDsa publicKey) CreateAsymmetricKeys(ECCurve curve)
	{
		var privateKey = ECDsa.Create(curve);
		var publicKey = ECDsa.Create(privateKey.ExportParameters(false));

		return (privateKey, publicKey);
	}
}