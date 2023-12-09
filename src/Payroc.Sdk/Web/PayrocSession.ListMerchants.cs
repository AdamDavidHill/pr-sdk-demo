using Payroc.Sdk.Errors;
using Payroc.Sdk.Models.QueryParameters;
using Payroc.Sdk.Models.Responses;

namespace Payroc.Sdk.Web;

internal partial class PayrocSession
{
    public Task<Result<IListMerchantPlatformsResponse>> ListMerchants(string? before = null, string? after = null, int limit = 10)
        => ListMerchants(new() { Before = before, After = after, Limit = limit }, CancellationToken.None);

    public Task<Result<IListMerchantPlatformsResponse>> ListMerchants(CancellationToken cancellationToken, string? before = null, string? after = null, int limit = 10)
        => ListMerchants(new() { Before = before, After = after, Limit = limit }, cancellationToken);

    internal async Task<Result<IListMerchantPlatformsResponse>> ListMerchants(ListMerchantParameters parameters, CancellationToken cancellationToken)
        => await EnsureAuthenticated(cancellationToken).ConfigureAwait(false) switch
        {
            { IsSuccess: true } => await _api.ListMerchants(_mutableState!, parameters, cancellationToken).ConfigureAwait(false),
            _ => Result<IListMerchantPlatformsResponse>.Fail(new NotAuthorizedError())
        };
}
