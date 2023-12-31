﻿using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;
using Payroc.Sdk.Models;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy
{
    public Task<Result> CreateMerchant(IOAuthSessionState session, IdempotencyKey idempotencyKey, Merchant merchant, CancellationToken cancellationToken)
    {
        Logger?.BeginScope(typeof(ApiProxy));
        Logger?.LogDebug("Creating Merchant {Id}", merchant.MerchantPlatformId);

        return CallCreateMerchant(session, idempotencyKey, merchant, cancellationToken);
    }

    private Task<Result> CallCreateMerchant(IOAuthSessionState session, IdempotencyKey idempotencyKey, Merchant merchant, CancellationToken cancellationToken)
        => _config.Value.Environment switch
        {
            PayrocEnvironment.Live => CallApi(session, HttpMethod.Post, PayrocUrls.Merchant, BodyFrom(merchant), CreateMerchantHeaders(idempotencyKey), cancellationToken),
            PayrocEnvironment.Test => CallApi(session, HttpMethod.Post, PayrocUrls.MerchantTest, BodyFrom(merchant), CreateMerchantHeaders(idempotencyKey), cancellationToken),
            _ => EmulatedCreateMerchant()
        };

    private string? BodyFrom(Merchant merchant)
    {
        // TODO: Implement creation of request Json / serialization

        return "dummy implementation";
    }

    private Dictionary<string, string> CreateMerchantHeaders(IdempotencyKey idempotencyKey)
        => new()
        {
            { "Content-Type", "application/Json" },
            { "Idempotency-Key", idempotencyKey.Value }
        };

    private Task<Result> EmulatedCreateMerchant()
        => Task.FromResult(Result.Ok());
}
