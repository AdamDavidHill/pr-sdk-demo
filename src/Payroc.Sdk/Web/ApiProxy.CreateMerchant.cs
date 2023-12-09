using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;
using Payroc.Sdk.Models;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy
{
    public Task<Result> CreateMerchant(IOAuthSessionState session, string idempotencyKey, Merchant merchant, CancellationToken cancellationToken)
    {
        _logger?.BeginScope(typeof(ApiProxy));
        _logger?.LogDebug("Creating Merchant {Id}", merchant.MerchantPlatformId);

        return CallCreateMerchant(session, idempotencyKey, merchant, cancellationToken);
    }

    private Task<Result> CallCreateMerchant(IOAuthSessionState session, string idempotencyKey, Merchant merchant, CancellationToken cancellationToken)
        => _config.Value.Environment switch
        {
            PayrocEnvironment.Live => CallApi(HttpMethod.Post, PayrocUrls.Merchant, BodyFrom(merchant), CreateMerchantHeaders(idempotencyKey), cancellationToken),
            PayrocEnvironment.Test => CallApi(HttpMethod.Post, PayrocUrls.MerchantTest, BodyFrom(merchant), CreateMerchantHeaders(idempotencyKey), cancellationToken),
            _ => EmulatedCreateMerchant()
        };

    private string? BodyFrom(Merchant merchant)
    {
        // TODO: Implement creation of request Json / serialization

        return "dummy implementation";
    }

    private Dictionary<string, string> CreateMerchantHeaders(string idempotencyKey)
        => new()
        {
            { "Content-Type", "application/Json" },
            { "Idempotency-Key", idempotencyKey }
        };

    private Task<Result> EmulatedCreateMerchant()
        => Task.FromResult(Result.Ok());
}
