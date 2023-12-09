namespace Payroc.Sdk.Web;

public interface IOAuthSessionState
{
    private const int MarginOfErrorMinutes = 3;

    string AccessToken { get; }
    string RefreshToken { get; }
    DateTimeOffset Expiry { get; }

    bool Valid => Expiry.UtcDateTime.AddMinutes(MarginOfErrorMinutes) < DateTimeOffset.UtcNow;
}
