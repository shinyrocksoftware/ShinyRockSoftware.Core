namespace Base.Constant;

public class AuthConstants
{
	public const int EMAIL_TOKEN_TTL_MINUTES = 5;

	public const string CLAIM_TYPE_SECURITY_STAMP = "AspNet.Identity.SecurityStamp";

	public const string CODE_AUTHORIZATION_ATTEMPT_COUNT = "AUTHORIZATION_ATTEMPT_COUNT";
	public const string CODE_AUTHORIZATION_LOCKED_OUT_END = "AUTHORIZATION_LOCKED_OUT_END";
	public const string CODE_AUTHORIZATION_LOCKED_OUT_TIME = "AUTHORIZATION_LOCKED_OUT_TIME";
	public const string CODE_AUTHORIZATION_MAX_ATTEMPT = "AUTHORIZATION_MAX_ATTEMPT";
	public const string CODE_INVALID_ASSERTION = "INVALID_ASSERTION";
	public const string CODE_INVALID_CALLBACK_URL = "INVALID_CALLBACK_URL";
	public const string CODE_INVALID_CAPTCHA = "INVALID_CAPTCHA_TOKEN";
	public const string CODE_INVALID_CLIENT_ID = "INVALID_CLIENT_ID";
	public const string CODE_INVALID_DATA = "INVALID_DATA";
	public const string CODE_INVALID_GRANT_TYPE = "INVALID_GRANT_TYPE";
	public const string CODE_INVALID_TOKEN = "INVALID_TOKEN";
	public const string CODE_LOCKED_OUT = "LOCKED_OUT";
	public const string CODE_MISSING_ASSERTION = "MISSING_ASSERTION";
	public const string CODE_UNAUTHORIZED = "UNAUTHORIZED";
	public const string CODE_MISSING_SCOPE = "MISSING_SCOPE";
	public const string CODE_NOT_EXISTED_WHITELIST_CALLBACK_URLS_CONFIG = "NOT_EXISTED_WHITELIST_CALLBACK_URLS_CONFIG";
	public const string CODE_NOT_EXISTED_USER_KEY = "NOT_EXISTED_USER_KEY";
	public const string CODE_UNCONFIRMED_EMAIL = "UNCONFIRMED_EMAIL";
	public const string CODE_WRONG_PASSWORD = "WRONG_PASSWORD";

	public const string CONNECT_TOKEN_API_REQUEST_PROPERTY_APPLICATION = "Application";
	public const string CONNECT_TOKEN_API_REQUEST_PROPERTY_USER = "User";
	public const string GRANT_TYPE_CONFIRM_THEN_LOGIN_REGISTRATION = "gt:" + AuthGrantTypeConstants.CONFIRM_THEN_LOGIN;
	public const string GRANT_TYPE_RESET_THEN_LOGIN_REGISTRATION = "gt:" + AuthGrantTypeConstants.RESET_THEN_LOGIN;
	public const string GRANT_TYPE_SOCIAL_REGISTRATION = "gt:" + AuthGrantTypeConstants.SOCIAL;

	public const string IDENTITY_AUTHENTICATION_SCHEME = "Bearer";

	public const string MESSAGE_AUTHORIZATION_ATTEMPT_COUNT = "The number of attempts for the current user";
	public const string MESSAGE_INVALID_CALLBACK_URL_0 = "Callback Url '{0}' is invalid";

	public const string MESSAGE_AUTHORIZATION_LOCKED_OUT_END = "The end UTC date time of the lock. After this time, the current user could action again";
	public const string MESSAGE_AUTHORIZATION_LOCKED_OUT_TIME = "The constant time value shows how long the lock would happens";
	public const string MESSAGE_AUTHORIZATION_MAX_ATTEMPT = "The constant integer value shows the maximum attempts could be made for the current user";
	public const string MESSAGE_CONFIRMED_USERNAME_0 = "Username {0} is confirmed already";
	public const string MESSAGE_EXISTED_DISPLAY_NAME = "Display name existed";
	public const string MESSAGE_EXISTED_SCOPE = "The scope provided exist";
	public const string MESSAGE_INVALID_ASSERTION = "The Assertion format is: <provider> <assertion_value>";
	public const string MESSAGE_INVALID_CAPTCHA = "Captcha is invalid";
	public const string MESSAGE_INVALID_CLIENT_ID = "The client id doesn't exist";
	public const string MESSAGE_INVALID_DATA = "Invalid data";
	public const string MESSAGE_INVALID_GRANT_TYPE = "Invalid 'grant_type'";		public const string MESSAGE_NOT_EXISTED_WHITELIST_CALLBACK_URLS_CONFIG = "The whitelist_callback_urls config is missing";

	public const string MESSAGE_INVALID_TOKEN = "Identity Token could not be verified with Provider or expiry";
	public const string MESSAGE_LOCKED_OUT = "User is locked out";
	public const string MESSAGE_MISSING_ASSERTION = "The mandatory 'assertion' parameter was missing";
	public const string MESSAGE_MISSING_AUTHORIZATION_INFO = "One of the following issues happened: username doesn't exist, the username/password combination is not correct, or invalid 'grant_type'";
	public const string MESSAGE_MISSING_SCOPE = "Requires 'scope'";
	public const string MESSAGE_NOT_EXISTED_USER_KEY_0 = "UserKey {0} doesn't exist. UserKey could be email, mobile, or Social account id";
	public const string MESSAGE_SUCCESS_AUTHORIZATION = "The user credentials are correct. Returns access_token";
	public const string MESSAGE_UNAUTHORIZED = "The user is un-authorized";
	public const string MESSAGE_UNCONFIRMED_EMAIL_0 = "The {0} isn't confirmed";
	public const string MESSAGE_WRONG_PASSWORD = "The username/password combination is not correct";

	public const string PARAM_EMAIL = "email";
	public const string PARAM_EXPIRES_AT = "exp";
	public const string PARAM_NAME = "name";
	public const string PARAM_PHONE_NUMBER = "phone_number";
	public const string PARAM_SCOPE = "scope";
	public const string PARAM_SOCIAL_UID = "social_uid";

	public const string CODE_CONFIRMED_USERNAME = "CONFIRMED_USERNAME";
	public const string CODE_INVALID_SCOPE = "INVALID_SCOPE";
	public const string CODE_NOT_EXISTED_APPLICATION = "NOT_EXISTED_APPLICATION";
	public const string CODE_NOT_EXISTED_ID = "NOT_EXISTED_ID";
	public const string CODE_NOT_EXISTED_SCOPE_NAME = "NOT_EXISTED_SCOPE_NAME";

	public const string IDENTITY_SCOPE_PREFIXED_0 = "scp:{0}";
	public const string MESSAGE_NOT_EXISTED_APPLICATION_0 = "Application {0} doesn't exist";
	public const string MESSAGE_NOT_EXISTED_SCOPE_0 = "Scope {0} doesn't exist";
	public const string MESSAGE_SAVE_APPLICATION_WARNING = "Please save this data, as it couldn't be shown again";
	public const string MESSAGE_SUCCESS_ACTIVATE = "Activate user successfully";

	public const string SERVICE_NAME = "ATS Authorization service";

	public const string SCOPE_RESOURCES_RECAPTCHA_SECRET_KEY_KEY = "recaptcha_secret_key";

	public const string STATUS_VALID = "valid";
	public const string TYPE_ADHOC = "ad-hoc";

	public const string AUTHENTICATION_CACHING_PREFIX = "Authentication";

	public const string CODE_CANT_DELETE_SCOPE_ID = "CAN_NOT_DELETE_SCOPE_ID";
	public const string CODE_CANT_EDIT_SCOPE_ID = "CAN_NOT_EDIT_SCOPE_ID";
	public const string CODE_CONFIRMED_EMAIL = "CONFIRMED_EMAIL";
	public const string CODE_CONFIRMED_PHONE_NUMBER = "CONFIRMED_PHONE_NUMBER";
	public const string CODE_EXISTED_CODE = "EXISTED_CODE";
	public const string CODE_EXISTED_SOCIAL_LINK_USER = "EXISTED_SOCIAL_LINK_USER";
	public const string CODE_EXISTED_USER_KEY = "EXISTED_USER_KEY";
	public const string CODE_EXISTED_USER_LINK_SOCIAL = "EXISTED_USER_LINK_SOCIAL";
	public const string CODE_INVALID_USERNAME = "INVALID_USERNAME";
	public const string CODE_MISSING_ORGANIZATION_ID = "MISSING_ORGANIZATION_ID";
	public const string CODE_MISSING_USERNAME = "MISSING_USERNAME";
	public const string CODE_NO_USER_WAS_KICKED = "NO_USER_WAS_KICKED";
	public const string CODE_NOT_EXISTED_ORGANIZATION = "NOT_EXISTED_ORGANIZATION";
	public const string CODE_NOT_EXISTED_SCOPE_ID = "NOT_EXISTED_SCOPE_ID";
	public const string CODE_NOT_EXISTED_USER_ID = "NOT_EXISTED_USER_ID";
	public const string CODE_NOT_EXISTED_USERNAME = "NOT_EXISTED_USERNAME";
	public const string CODE_RESTRICTION_INVALID_PURPOSE = "RESTRICTION_INVALID_PURPOSE";
	public const string CODE_SAME_PASSWORD = "SAME_PASSWORD";

	public const string MESSAGE_CANT_DELETE_SCOPE_0 = "Scope {0} can't delete";
	public const string MESSAGE_CANT_EDIT_SCOPE_0 = "Scope {0} can't edit";
	public const string MESSAGE_CONFIRMED_EMAIL = "Email is confirmed already";
	public const string MESSAGE_CONFIRMED_PHONE_NUMBER = "Phone number is confirmed already";
	public const string MESSAGE_EXISTED_CODE_0 = "Code {0} existed";
	public const string MESSAGE_EXISTED_NAME = "Name existed";
	public const string MESSAGE_EXISTED_SOCIAL_0_LINK_USER = "{0} social account already linked to another account";
	public const string MESSAGE_EXISTED_USER_0_LINK_SOCIAL_1 = "Username {0} already linked to another {1} social account";
	public const string MESSAGE_EXISTED_USER_KEY_0 = "User key {0} existed";
	public const string MESSAGE_INVALID_USERNAME = "Invalid 'username'. Username must be email";
	public const string MESSAGE_MISSING_ORGANIZATION_ID = "Requires 'organizationId'";
	public const string MESSAGE_MISSING_USERNAME = "Requires 'username'";
	public const string MESSAGE_NO_USER_WAS_KICKED = "Not any user was kicked";
	public const string MESSAGE_NOT_EXISTED_ORGANIZATION = "Organization doesn't exist";
	public const string MESSAGE_NOT_EXISTED_ORGANIZATION_0 = "Organization {0} doesn't exist'";
	public const string MESSAGE_NOT_EXISTED_USER_ID = "User Id doesn't exist";
	public const string MESSAGE_NOT_EXISTED_USER_ID_0 = "User Id {0} doesn't exist";
	public const string MESSAGE_NOT_EXISTED_USERNAME = "Username doesn't exist";
	public const string MESSAGE_NOT_EXISTED_USERNAME_0 = "Username {0} doesn't exist";
	public const string MESSAGE_RESTRICTION_INVALID_PURPOSE = "Purpose is invalid";
	public const string MESSAGE_SAME_PASSWORD = "The new password must be different than the current password";
	public const string MESSAGE_SUCCESS_CHANGING_PASSWORD = "Password is changed successfully";
	public const string MESSAGE_SUCCESS_CONFIRMATION = "Confirmation is successful";
	public const string MESSAGE_SUCCESS_DEACTIVATE = "Deactivate user successfully";
	public const string MESSAGE_SUCCESS_FORGETTING_PASSWORD = "Forgot Password is called successfully";
	public const string MESSAGE_SUCCESS_FORGOT_PASSWORD = "Forgot password call is successful";
	public const string MESSAGE_SUCCESS_GET_ACTIVE_USERS = "Get active users successfully";
	public const string MESSAGE_SUCCESS_GET_SOCIAL_ACCOUNTS = "Get social accounts successfully";
	public const string MESSAGE_SUCCESS_IS_AUTHORIZED = "The user is authorized";
	public const string MESSAGE_SUCCESS_KICK_OUT_USER = "kick out users successfully";
	public const string MESSAGE_SUCCESS_LINK_ACCOUNT = "Link account successfully";
	public const string MESSAGE_SUCCESS_REGISTRATION = "Registration is successful";
	public const string MESSAGE_SUCCESS_RESEND_CONFIRMATION = "Successful resend confirmation";
	public const string MESSAGE_SUCCESS_RESETTING_PASSWORD = "Password is reset successfully";
	public const string MESSAGE_SUCCESS_UNLINK_SOCIAL_ACCOUNT = "Unlink social account successfully";
	public const string MESSAGE_SUCCESS_VERIFY_CONFIRMATION = "Verify confirmation token successfully";
	public const string MESSAGE_SUCCESS_VERIFY_LINK_ACCOUNT = "Verify link account successfully";
	public const string MESSAGE_SUCCESS_VERIFY_PASSWORD_RESET = "Verify password reset token successfully";
	public const string MESSAGE_UNKNOWN_ERROR = "Unknown error";

	public const string PARAM_APP_PLATFORM = "app_platform";
	public const string PARAM_APP_VERSION = "app_version";
	public const string PARAM_DEVICE_ID = "device_id";
	public const string PARAM_IS_SOCIAL = "is_social";
	public const string PARAM_ORGANIZATION_CODE = "organization_code";
	public const string PARAM_ORGANIZATION_ID = "organization_id";
	public const string PARAM_SOCIAL_TYPE = "social_type";

	public const string SEPARATOR = "__";

	public const string SOCIAL_USER_NAME_TEMPLATE = "{0}_{1}";
}