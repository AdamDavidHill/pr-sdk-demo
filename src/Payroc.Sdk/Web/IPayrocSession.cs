using Payroc.Sdk.Models;
using Payroc.Sdk.Models.Responses;

namespace Payroc.Sdk.Web;

public interface IPayrocSession
{
    Task<Result> CreateMerchant(string idempotencyKey, Merchant merchant);
    Task<Result> CreateMerchant(string idempotencyKey, Merchant merchant, CancellationToken cancellationToken);
    Task<Result<IListMerchantPlatformsResponse>> ListMerchants(string? before = null, string? after = null, int limit = 10);
    Task<Result<IListMerchantPlatformsResponse>> ListMerchants(CancellationToken cancellationToken, string? before = null, string? after = null, int limit = 10);
    string CreateIdempotencyToken() => Guid.NewGuid().ToString().ToUpper();
}
