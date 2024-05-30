﻿using System.Security.Cryptography;
using Core.Configuration.Interface;
using Core.Constant;
using Core.Extension;
using Core.Jwt.Interface;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Core.Jwt.Tests;

public class JwtHelpersTests
{
	private IJwtHelper _jwtHelper;

	[SetUp]
	public void SetUp()
	{
		using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
		var logger = loggerFactory.CreateLogger<JwtHelper>();

		var connectorModelHelper = Substitute.For<IConnectorModelHelper>();
		_jwtHelper = new JwtHelper(logger, connectorModelHelper);
	}

	[Test]
	public void SymmetricEncode_Generate_Jwt()
	{
		var payload = new Dictionary<string, object>
		{
			{ "Key 1", "Value 1" }
		};
		var secretKey = StringExtensions.Random(64);

		var jwt = _jwtHelper.SymmetricEncode(payload, secretKey, JwtConstants.SymmetricAlgorithm.HS256);

		ClassicAssert.IsTrue(jwt.IsJwtFormat());
	}

	[Test]
	public void SymmetricDecode_GetOriginal_Payload()
	{
		var payload = new Dictionary<string, object>
		{
			{ "Key 1", "Value 1" },
			{ "Key 2", "Value 1" },
			{ "Key 3", "Value 1" },
			{ "Key 4", "Value 1" },
			{ "Key 5", "Value 1" },
		};
		var secretKey = StringExtensions.Random(64);

		var jwt = _jwtHelper.SymmetricEncode(payload, secretKey, JwtConstants.SymmetricAlgorithm.HS256);

		var decodedPayload = _jwtHelper.SymmetricDecode(jwt, secretKey);

		ClassicAssert.AreEqual("Value 1", decodedPayload["Key 5"]);
	}

	[Test]
	public void AsymmetricEncode_Generate_Jwt()
	{
		var payload = new Dictionary<string, object>
		{
			{ "Key 1", "Value 1" }
		};

		(ECDsa privateKey, ECDsa _) = _jwtHelper.CreateAsymmetricKeys(ECCurve.NamedCurves.nistP256);
		var jwt = _jwtHelper.AsymmetricEncode(privateKey, JwtConstants.AsymmetricAlgorithm.ES512, payload);

		ClassicAssert.IsTrue(jwt.IsJwtFormat());
	}

	[Test]
	public void AsymmetricDecode_GetOriginal_Payload()
	{
		var payload = new Dictionary<string, object>
		{
			{ "Key 1", "Value 1" },
			{ "Key 2", "Value 1" },
			{ "Key 3", "Value 1" },
			{ "Key 4", "Value 1" },
			{ "Key 5", "Value 1" },
		};

		(ECDsa privateKey, ECDsa publicKey) = _jwtHelper.CreateAsymmetricKeys(ECCurve.NamedCurves.nistP256);
		var jwt = _jwtHelper.AsymmetricEncode(privateKey, JwtConstants.AsymmetricAlgorithm.ES512, payload);

		var decodedPayload = _jwtHelper.AsymmetricDecode(jwt, publicKey);

		ClassicAssert.AreEqual("Value 1", decodedPayload["Key 5"]);
	}
}