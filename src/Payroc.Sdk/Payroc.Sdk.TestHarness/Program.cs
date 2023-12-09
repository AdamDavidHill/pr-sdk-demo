using Microsoft.Extensions.DependencyInjection;
using Payroc.Sdk;
using Payroc.Sdk.TestHarness;

var examples = new[]
{
    ConfigExamples.HostSetupExample1(),
    ConfigExamples.HostSetupExample2(),
    ConfigExamples.HostSetupExample3(),
    ConfigExamples.HostSetupExample4()
};
var indexedExamples = examples.Select((i, index) => (Index: index, Host: i));

foreach (var example in indexedExamples)
{
    Console.WriteLine("Example {0}", example.Index + 1);
    var payroc = example.Host.Services.GetRequiredService<IPayrocService>();

    // Simple call
    var merchant1 = TestDataGenerator.CreateExampleMerchant1();
    var result = await payroc.CreateSession().CreateMerchant(payroc.CreateIdempotencyToken(), merchant1);

    // Longer-lived session re-use
    var merchant2 = TestDataGenerator.CreateExampleMerchant2();
    var session = payroc.CreateSession();
    var token = payroc.CreateIdempotencyToken();
    var createdResult = await session.CreateMerchant(token, merchant2);

    if (createdResult.IsSuccess)
    {
        var listMerchantsResult = await session.ListMerchants(token); // Re-using same session reference for followup call
        
        if (listMerchantsResult.IsSuccess)
        {
            foreach (var merchant in listMerchantsResult.Content.Data)
            {
                // Won't actually output data ATM, as implementation is mocked, so no records stored/retrieved for real
                Console.WriteLine("Found Merchant {MerchantId}", merchant.MerchantPlatformId);
            }
        }
    }

    Console.WriteLine("Example {0} End", example.Index + 1);
}

Console.WriteLine("Demo complete. Hit any key to end.");
Console.ReadLine();
