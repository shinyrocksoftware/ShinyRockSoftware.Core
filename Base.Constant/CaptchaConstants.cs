namespace Base.Constant;

public static class CaptchaConstants
{
    public const string URL_VALIDATE_TOKEN_GOOGLE_CAPTCHA= "https://www.google.com/recaptcha/api/";
    public const string ENDPOINT_VALIDATE_TOKEN_GOOGLE_CAPTCHA = "siteverify?secret={0}&response={1}";
}