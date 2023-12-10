using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payroc.Sdk.Config;
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
    // Also if the state was essentially global rather than scoped, the user may trip up with conflicting integration tests
    public IPayrocSession CreateSession()
        => new PayrocSession(_api);

    // For clients who don't use DI, we have a static factory method
    public static IPayrocService Create(PayrocOptions options, HttpClient httpClient, ILogger? logger = null)
        => new PayrocService(new ApiProxy(Options.Create(options), PayrocLoggerFactory.Create(logger), PayrocHttpClientFactory.Create(httpClient)));
}
