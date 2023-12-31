﻿using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;
using Payroc.Sdk.Models.QueryParameters;
using Payroc.Sdk.Models.Responses;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy
{
    public Task<Result<IListMerchantPlatformsResponse>> ListMerchants(IOAuthSessionState session, IListMerchantParameters queryParameters, CancellationToken cancellationToken)
    {
        Logger?.BeginScope(typeof(ApiProxy));
        Logger?.LogDebug("Getting up to {Limit} Merchant Platforms", queryParameters.Limit);
        
        return CallListMerchants(session, queryParameters, cancellationToken);
    }

    private Task<Result<IListMerchantPlatformsResponse>> CallListMerchants(IOAuthSessionState session, IListMerchantParameters parameters, CancellationToken cancellationToken)
        => _config.Value.Environment switch
        {
            PayrocEnvironment.Live => CallApi<IListMerchantPlatformsResponse>(session, HttpMethod.Get, UrlFrom(PayrocUrls.Merchant, parameters), cancellationToken),
            PayrocEnvironment.Test => CallApi<IListMerchantPlatformsResponse>(session, HttpMethod.Get, UrlFrom(PayrocUrls.MerchantTest, parameters), cancellationToken),
            _ => EmulatedListMerchants()
        };

    private string UrlFrom(string baseUrl, IListMerchantParameters parameters)
    {
        // TODO: Format URL with query string

        return "dummy implementation";
    }

    private Task<Result<IListMerchantPlatformsResponse>> EmulatedListMerchants()
        => Task.FromResult(Result<IListMerchantPlatformsResponse>.Ok(new ListMerchantPlatformsResponse()));
}
