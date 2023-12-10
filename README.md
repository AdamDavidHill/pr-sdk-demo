# pr-sdk-demo

A demonstration of SDK design thinking, in relation to the [Payroc Merchant APIs](https://docs.payroc.com/api).

> [!WARNING]  
> This code is purely illustrative, presented in order to communicate SDK design ideas. None of it ought to be considered tested, or ready for use. 

## Overview

Although Nuget would be used in a real scenario, for this demo a Test Harness project with a project reference is employed. It demonstrates usage of the SDK.

### Recommended Browsing (Client's Eye View)

| File                                                                     | What it demonstrates |
| --- | --- |
| [Demo1.cs](/src/Payroc.Sdk.TestHarness/Demo1.cs)                         | Simple non-DI-based SDK use |
| [Demo2.cs](/src/Payroc.Sdk.TestHarness/Demo2.cs)                         | More elaborate SDK use (with DI) |
| [DIConfig.cs](/src/Payroc.Sdk.TestHarness/DIConfig.cs)                   | Options for wiring up DI config |
| [TestDataGenerator.cs](/src/Payroc.Sdk.TestHarness/TestDataGenerator.cs) | Fluent Builder-like helpers for creating models |

### Recommended Browsing (Implementation Highlights)

| File                                                                                                      | What it demonstrates |
| --- | --- |
| [PayrocService.cs](/src/Payroc.Sdk/PayrocService.cs)                                                      | Main SDK "entry point" |
| [IServiceCollectionExtensions.cs](/src/Payroc.Sdk/Config/Extensions/IServiceCollectionExtensions.cs)      | Internals of DI approaches |
| [PayrocSession.Authentication.cs](/src/Payroc.Sdk/Web/PayrocSession.Authentication.cs)                    | Caching tokens, and passing API calls through `EnsureAuthenticated` to hide OAuth complexity |
| [IPayrocSession.cs](/src/Payroc.Sdk/Web/IPayrocSession.cs)                                                | Typical top-level UX of an API call |
| [ApiProxy.cs](/src/Payroc.Sdk/Web/ApiProxy.cs)                                                            | Main guts of API calls (note adjacent `partial class` variations |
| [ApiProxy.cs](/src/Payroc.Sdk/Web/ApiProxy.cs)                                                            | Main guts of API calls (note adjacent `partial class` variations |
| [PayrocLoggerFactory.cs](/src/Payroc.Sdk/Dependencies/PayrocLoggerFactory.cs)                             | How to allow both DI loggers and manual construction |

## Getting Started (Using the SDK)

### IConfiguration Setup Version

This approach lets the user provide their configuration via IConfiguration, such as `appsettings.json`, and/or environment variables loaded into `IConfiguration` etc.

Configuration in `appsettings.json`:

```json
{
  "Payroc": {
    "ApiKey": "abc123",
    "Environment": "Test"
  }
}
```

As per [MS guidance](https://learn.microsoft.com/en-us/dotnet/core/extensions/options-library-authors#iconfiguration-parameter), an extension method, `AddPayroc`, provides a simple hook to handle all the configuration.
[DIConfig.cs](/src/Payroc.Sdk.TestHarness/DIConfig.cs) demonstrates four variations/overloads.

#### Pre-requisite

```csharp
using Payroc.Sdk.Config.Extensions;
using Microsoft.Extensions.Hosting;
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
```

#### Overload 1
```csharp
builder.Services.AddPayroc(builder.Configuration.GetSection("Payroc"));
```

#### Overload 2
```csharp
builder.Services.AddPayroc(new PayrocOptions
                            {
                                ApiKey = "test",
                                Environment = PayrocEnvironment.LocalEmulation
                            });
```

#### Overload 3
```csharp
builder.Services.AddPayroc("Payroc"); // Name of the config section in `appsettings.json`
```

#### Overload 4
```csharp
builder.Services.AddPayroc(options =>
                            {
                                options.ApiKey = "test";
                                options.Environment = PayrocEnvironment.LocalEmulation;
                            });
```

## Small Details

Some details which might be easy to miss:

### Floating Version References

- We use floating references for Nuget dependencies, e.g. `Version="8.*"`, to be more flexible.
- We trigger validation of the config on startup, as the SDK can't function successfully if misconfigured
- `PayrocEnvironment` implies building an emulator mode directly into the SDK, so the end user doesn't need to manage an Emulator container etc.


## Bits I'm Not 100% Happy With

### Plain ILogger

I don't love the trade off mentioned in [PayrocLoggerFactory.cs](/src/Payroc.Sdk/Dependencies/PayrocLoggerFactory.cs), so might be worth more thought around this.

### 3rd Party Dependency Injection

I'd definitely need to test how the DI setup patterns work with 3rd Party DI libraries like [Lamar](https://jasperfx.github.io/lamar/). If this were a problem, then additional package variations could be created offering specific support for those libraries.

### Idempotency Key

At a glance I'm not sure where's best to put creation of this. I could see any of these being reasonable, depending on use case:

- Force user to knowingly create it via `IdempotencyKey.New()`, which gives them a reference they can store
- Hide it internally, if we're confident that normal SDK use doesn't warrant it
- Optionally allow user to specify / store, and otherwise handle it internally

## Ideas for Improvement

### Logging Function

Offer support to provide an anonymous logging function instead of an `ILogger`. Might muddy the signatures, but could be handy for obscure logging behaviours.

### Multi-targeting

Support all LTS versions (currently 6 & 8), and potentially one recent non-LTS version.

This can sometimes be as simple as changing:
```xml
 <TargetFramework>net8.0</TargetFramework>
```
to:
```xml
 <TargetFrameworks>net6.0;net7.0;net8.0</TargetFrameworks>
```
Depending on usage of recent .Net features internally.

### Singleton OAuth & Single-Threaded Authentication

Rather than scope-specific sessions / auth tokens, we could go for a global single reusable state. To prevent unnecessary Auth calls we could limit this to blocking access with a SemaphoreSlim. This might improve performance in terms of reducing unnecessary auth calls from independent parts of a user's application, but might trip up their parallel testing. Part of the design at the moment is to delegate this issue to the user, by giving them a handle on the `IPayrocSession` object, which they can choose how to handle it.

### Model Builders

My [Fluent Builder](https://betterprogramming.pub/improve-code-readability-with-fluent-builder-pattern-in-c-4cfbf57159df) implementation is a bit of a quick hack at the moment. This could adopt a more formal builder pattern, with a builder class and an explicit final `Build()` step. This is perhaps more conventional, but I don't mind the current one too much.

```csharp
var model = new MerchantBuilder()
    .WithId("123456")
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
var model = new MerchantBuilder()
    .WithId("123456")
    .WithBusiness(name: "Example corp", taxId: "xxx-xx-4321", organizationType: "privateCorporation", countryOfOperation: "US")
    .WithAddress(addr1)
    .WithContactMethod(ContactMethodType.Phone, "123 456 7890")
    .ValidatingBuild();
```

### /// XML docs 

Although it's a maintenance burden, full documentation of methods, types etc. can be helpful provided it's correct. This can enrich intellisense on the user's side.

e.g. [XML Docs within a Nuget package](https://stackoverflow.com/questions/5205738/how-do-you-include-xml-docs-for-a-class-library-in-a-nuget-package)

### Misc Ongoing Checks

- Check `.ConfigureAwait(false)` when `await`ing inside the SDK
- Check AOT compatibility
