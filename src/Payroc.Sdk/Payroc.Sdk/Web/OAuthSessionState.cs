namespace Payroc.Sdk.Web;

internal record OAuthSessionState(string AccessToken, string RefreshToken, DateTimeOffset Expiry) 
    : IOAuthSessionState;
