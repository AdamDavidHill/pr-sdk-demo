namespace Payroc.Sdk.Web;

internal partial class PayrocSession : IPayrocSession
{
    private readonly IApiProxy _api;

    public PayrocSession(IApiProxy api)
        => _api = api;
}
