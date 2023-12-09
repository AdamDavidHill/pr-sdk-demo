namespace Payroc.Sdk.Config;

public class PayrocOptions
{
    public string? ApiKey { get; set; }

    // Some sort of config or flag to allow end-users to switch out environments simply
    // Real URLs can be tucked away, interned within the SDK itself, as the version of an SDK is coupled with an API version anyway
    // No point bothering the end users with specifying the URLs for each
    // Local could be an in-memory emulator built inside the SDK or a local container
    public PayrocEnvironment Environment { get; set; } = PayrocEnvironment.LocalEmulation;
}
