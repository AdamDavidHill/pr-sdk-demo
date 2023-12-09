# pr-sdk-demo

A demonstration of SDK design thinking, in relation to the Payroc Merchant APIs:

- https://docs.payroc.com/api/resources#createMerchant
- https://docs.payroc.com/api/resources#listMerchantPlatforms

## Warning

None of this code is presented as tested, working or fit for use. It is purely illustrative, presented in order to communicate SDK design ideas.

## Getting Started

Although Nuget would be used in a real scenario, for this demo, the Test Harness project has a direct project reference to the SDK project. It demonstrates usage of the SDK.

```xml
<ItemGroup>
    <ProjectReference Include="..\Payroc.Sdk\Payroc.Sdk.csproj" />
</ItemGroup>
```

### IConfiguration Setup Version

This approach lets the user provide their configuration via IConfiguration, such as AppSettings, and/or environment variables etc.

Configuration would look as follows:

```json
{
  "Payroc": {
    "ApiKey": "abc123",
    "Environment": "Test"
  }
}
```

As per [MS guidance](https://learn.microsoft.com/en-us/dotnet/core/extensions/options-library-authors#iconfiguration-parameter), an extension method, `AddPayroc`, provides a simple setup in `Program.cs`:

```csharp
using Payroc.Sdk.Config.Extensions;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddPayroc(builder.Configuration.GetSection("Payroc"));
```

## Ideas

### Logging

Have safe ILoggerFactory implementation by default.
Enable overriding of logging via passing in function.
Offer sync & async functions? Most logging implementations are synchronous, so might be barrier to entry for some users declaring async lambdas.

### Multi-targeting

Support all LTS versions (6 + 8), and potentially the most recent non-LTS version.

This can sometimes be as simple as changing:
```xml
 <TargetFramework>net8.0</TargetFramework>
```
to
```xml
 <TargetFrameworks>net6.0;net7.0;net8.0</TargetFrameworks>
```
Depending on usage of recent .Net features.

### Add Semophore to AuthenticationService

To prevent unnecessary Auth calls we could limit this to blocking access.

### Model Builders

- Implement [Fluent Builder Pattern](https://betterprogramming.pub/improve-code-readability-with-fluent-builder-pattern-in-c-4cfbf57159df) to make creation of deep models easier / cleaner

```csharp
var model = new Merchant(merchantPlatformId: "123456")
    .WithBusiness(name: "Example corp", taxId: "xxx-xx-4321", organizationType: "privateCorporation", countryOfOperation: "US")
    .WithAddress(addr1)
    .WithAddress(addr2)
    .WithContactMethod(ContactMethodType.Phone, "123 456 7890")
    .Build();
```

### Model Validators

- Create validation helpers for Models, e.g. verify at least one ContactMethod appears on a Business prior to calling the actual API
- Could be based on OpenApi spec for easier long term maintenance / cross-platform consistency
- Implement so can be chained into the Fluent Builder process mentioned before

e.g.

```csharp
var modelValidationResult = new Merchant(merchantPlatformId: "123456")
    .WithBusiness(name: "Example corp", taxId: "xxx-xx-4321", organizationType: "privateCorporation", countryOfOperation: "US")
    .WithAddress(addr1)
    .WithContactMethod(ContactMethodType.Phone, "123 456 7890")
    .Build()
    .Validate();
```

### /// XML docs 

Although it's a maintenance burden, full documentation of methods, types etc. can be helpful provided it's correct. This can enrich intellisense on the user's side.

e.g. [XML Docs within a Nuget package](https://stackoverflow.com/questions/5205738/how-do-you-include-xml-docs-for-a-class-library-in-a-nuget-package)

### Misc

- Check `.ConfigureAwait(false)` when `await`ing
