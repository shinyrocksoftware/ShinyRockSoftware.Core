using Core.Model.Interface;

namespace Core.Captcha.Interfaces;

public interface ICaptchaHelper : IAutoInjection
{
    bool IsValid(string secretKey, string token);
    Task<bool> IsValidAsync(string secretKey, string token, CancellationToken cancellationToken);
}