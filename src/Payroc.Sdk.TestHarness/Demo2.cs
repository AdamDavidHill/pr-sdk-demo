using Microsoft.Extensions.DependencyInjection;
using Payroc.Sdk;
using Payroc.Sdk.Models;
using Payroc.Sdk.TestHarness;

internal class Demo2
{
    public async Task Run()
    {
        Console.WriteLine("Executing examples based on Dependency Injection...");
        
        foreach (var example in DIConfig.Examples)
        {
            Console.WriteLine("Example {0}", example.Index);
            var payroc = example.Host.Services.GetRequiredService<IPayrocService>();

            await SimpleExample(payroc);
            await SessionReuseExample(payroc);
            await ErrorHandlingExample(payroc);
        }
    }

    private Task SimpleExample(IPayrocService payroc)
        => payroc
            .CreateSession()
            .CreateMerchant(payroc.CreateIdempotencyKey(), Data1);

    private async Task SessionReuseExample(IPayrocService payroc)
    {
        var session = payroc.CreateSession();
        _ = await session.CreateMerchant(payroc.CreateIdempotencyKey(), Data2);
        _ = await session.ListMerchants(payroc.CreateIdempotencyKey()); // Re-using same session reference for followup call
    }

    private async Task ErrorHandlingExample(IPayrocService payroc)
    {
        var session = payroc.CreateSession();
        var createdResult = await session.CreateMerchant(payroc.CreateIdempotencyKey(), Data2);

        if (createdResult.IsSuccess)
        {
            var listMerchantsResult = await session.ListMerchants(payroc.CreateIdempotencyKey()); // Re-using same session reference for followup call

            if (listMerchantsResult.IsSuccess)
            {
                foreach (var merchant in listMerchantsResult.Content.Data)
                {
                    // Won't actually output data ATM, as implementation is mocked, so no records stored/retrieved for real
                    Console.WriteLine("Found Merchant {MerchantId}", merchant.MerchantPlatformId);
                }
            }
            else
            {
                var error = listMerchantsResult.Error;
                Console.WriteLine("Non-Exceptional error encountered: {Error} {Title} {Details}", error.SdkError, error.Title, error.Details);
            }
        }
        else
        {
            var error = createdResult.Error;
            Console.WriteLine("Non-Exceptional error encountered: {Error} {Title} {Details}", error.SdkError, error.Title, error.Details);
        }
    }

    private Merchant Data1 => TestDataGenerator.CreateExampleMerchant1();

    private Merchant Data2 => TestDataGenerator.CreateExampleMerchant2();
}
