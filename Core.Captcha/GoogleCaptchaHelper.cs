using System.Text.Json;
using Core.Attribute.AutoInjection;
using Core.Captcha.Interfaces;
using Base.Constant;
using Base.Extension;
using Core.Helper;
using Microsoft.Extensions.Logging;

namespace Core.Captcha;

[SingletonAutoInjection]
internal class GoogleCaptchaHelper : ICaptchaHelper
{
	private readonly ILogger _logger;

	public GoogleCaptchaHelper(ILogger<GoogleCaptchaHelper> logger)
	{
		_logger = logger;
	}

	public bool IsValid(string secretKey, string token)
	{
		var fullEndpoint = GetFullEndpoint(secretKey, token, out var endpoint);

		var response = RestHelper.Get<string>(fullEndpoint);
		return ReturnResult(token, response.Data, endpoint);
	}

	public async Task<bool> IsValidAsync(string secretKey, string token, CancellationToken cancellationToken)
	{
		var fullEndpoint = GetFullEndpoint(secretKey, token, out var endpoint);

		var response = await RestHelper.GetAsync<string>(fullEndpoint, cancellationToken);
		return ReturnResult(token, response.Data, endpoint);
	}

	private string GetFullEndpoint(string secretKey, string token, out string endpoint)
	{
		var baseUri = CaptchaConstants.URL_VALIDATE_TOKEN_GOOGLE_CAPTCHA.SafeEndpoint();
		endpoint = CaptchaConstants.ENDPOINT_VALIDATE_TOKEN_GOOGLE_CAPTCHA.SafeEndpoint().ApplyFormat(secretKey, token);

		return $"{baseUri}/{endpoint}";
	}

	private bool ReturnResult(string token, string response, string endpoint)
	{
		var jsonData = response.Deserialize<JsonElement>();
		var result = jsonData.GetProperty("success").GetBoolean();

		if (!result)
		{
			_logger.LogError($"Captcha validation failed. Relate data: endpoint = {endpoint} , token = {token}, data={jsonData}");
		}

		return result;
	}
}