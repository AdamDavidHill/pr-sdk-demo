using Microsoft.Extensions.Options;

namespace Payroc.Sdk.Config;

public class PayrocConfigValidator : IValidateOptions<PayrocOptions>
{
    public ValidateOptionsResult Validate(string? name, PayrocOptions options)
        => options switch
        {
            null => ValidateOptionsResult.Fail("Payroc SDK Configuration object is null."),
            { ApiKey: null } => ValidateOptionsResult.Fail("Payroc SDK Configuration object is missing a value for ApiKey."),

            // etc. Validate other config aspects as required...

            _ => ValidateOptionsResult.Success
        };
}
