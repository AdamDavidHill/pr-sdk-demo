using Payroc.Sdk.Web;

namespace Payroc.Sdk;

// https://docs.payroc.com/api
// https://docs.payroc.com/api/resources#createMerchant
// https://docs.payroc.com/api/resources#listMerchantPlatforms

// Note: I typically don't like to add `sealed` onto this sort of thing, as it limits users' options for Mocking, although the interface helps
public class PayrocService : IPayrocService
{
    private readonly IApiProxy _api;

    public PayrocService(IApiProxy api)
        => _api = api;

    // The reason I leak the reference to the session is to allow the user to potentially re-use the same session for multiple calls
    // Otherwise we fully authenticate from scratch each time
    public IPayrocSession CreateSession()
        => new PayrocSession(_api);
}
