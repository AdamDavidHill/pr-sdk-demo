using Microsoft.Extensions.Logging;
using Payroc.Sdk;
using Payroc.Sdk.Config;
using Payroc.Sdk.Models;

internal class Demo1
{
    public async Task Run()
    {
        Console.WriteLine("Executing example without Dependency Injection...");

        var payroc = PayrocService.Create(Config, HttpClient, Logger);
        await payroc
                .CreateSession()
                .CreateMerchant(IdempotencyKey.New(), Data);
    }

    // Remaining code in this file is quick & dirty helpers for demo purposes just to clean up the code above
    private static Merchant Data => TestDataGenerator.CreateExampleMerchant1();
    private static PayrocOptions Config => new() { ApiKey = "abc", Environment = PayrocEnvironment.LocalEmulation };
    private ILogger Logger => LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
    private HttpClient HttpClient => new();
}
