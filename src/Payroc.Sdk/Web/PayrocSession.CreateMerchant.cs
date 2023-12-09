using Payroc.Sdk.Errors;
using Payroc.Sdk.Models;

namespace Payroc.Sdk.Web;

internal partial class PayrocSession
{
    public Task<Result> CreateMerchant(string idempotencyKey, Merchant merchant)
        => CreateMerchant(idempotencyKey, merchant, CancellationToken.None);

    public async Task<Result> CreateMerchant(string idempotencyKey, Merchant merchant, CancellationToken cancellationToken)
        => await EnsureAuthenticated(cancellationToken).ConfigureAwait(false) switch
        {
            { IsSuccess: true } => await _api.CreateMerchant(_mutableState!, idempotencyKey, merchant, cancellationToken).ConfigureAwait(false),
            _ => Result.Fail(new NotAuthorizedError())
        };
}
