using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy
{
    public Task<Result<IOAuthSessionState>> Authenticate(CancellationToken cancellationToken)
    {
        _logger?.BeginScope(typeof(ApiProxy));
        _logger?.LogDebug("Authenticating Payroc session.");

        return AuthenticateForEnvironment(cancellationToken);
    }

    private Task<Result<IOAuthSessionState>> AuthenticateForEnvironment(CancellationToken cancellationToken)
        => _config.Value.Environment switch
        {
            PayrocEnvironment.Live => CallApi<IOAuthSessionState>(HttpMethod.Post, PayrocUrls.Authorize, cancellationToken),
            PayrocEnvironment.Test => CallApi<IOAuthSessionState>(HttpMethod.Post, PayrocUrls.AuthorizeTest, cancellationToken),
            _ => EmulatedAuthenticate()
        };

    private Task<Result<IOAuthSessionState>> EmulatedAuthenticate()
        => Task.FromResult(Result<IOAuthSessionState>.Ok(new OAuthSessionState("an access token", "a refresh token", DateTimeOffset.UtcNow.AddMinutes(30))));
}