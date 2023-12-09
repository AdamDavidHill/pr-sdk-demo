using Payroc.Sdk.Models;
using Payroc.Sdk.Models.QueryParameters;
using Payroc.Sdk.Models.Responses;

namespace Payroc.Sdk.Web;

public interface IApiProxy
{
    Task<Result<IOAuthSessionState>> Authenticate(CancellationToken cancellationToken);
    Task<Result> CreateMerchant(IOAuthSessionState session, string idempotencyKey, Merchant merchant, CancellationToken cancellationToken);
    Task<Result<IListMerchantPlatformsResponse>> ListMerchants(IOAuthSessionState session, IListMerchantParameters queryParameters, CancellationToken cancellationToken);
}
