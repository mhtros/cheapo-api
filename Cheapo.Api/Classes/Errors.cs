namespace Cheapo.Api.Classes;

public static class Errors
{
    public const string ExpiredToken = "EXPIRED_TOKEN";
    public const string ExpiredRefreshToken = "EXPIRED_REFRESH_TOKEN";
    public const string UserAlreadyExists = "USER_ALREADY_EXISTS";
    public const string EmailNotSend = "EMAIL_NOT_SEND";
    public const string InvalidToken = "INVALID_TOKEN";
    public const string AccountNotVerified = "ACCOUNT_NOT_VERIFIED";
    public const string NotValidTwoFactorToken = "NOT_VALID_TWO_FACTOR_TOKEN";
}