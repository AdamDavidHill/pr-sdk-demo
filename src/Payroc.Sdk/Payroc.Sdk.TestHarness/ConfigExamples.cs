using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;
using Payroc.Sdk.Config.Extensions;

namespace Payroc.Sdk.TestHarness;

internal class ConfigExamples
{
    public static IHost HostSetupExample1()
        => Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services
                            .AddHttpClient()
                            .AddLogging(logging => logging.AddConsole())
                            .AddPayroc(new PayrocOptions //                                             <---- Config hook
                            {
                                ApiKey = "test",
                                Environment = PayrocEnvironment.LocalEmulation
                            });
                    })
                    .Build();

    public static IHost HostSetupExample2()
        => Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services
                            .AddHttpClient()
                            .AddLogging(logging => logging.AddConsole())
                            .AddPayroc(context.Configuration.GetSection("Payroc")); //                  <---- Config hook
                    })
                    .Build();

    public static IHost HostSetupExample3()
        => Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services
                            .AddHttpClient()
                            .AddLogging(logging => logging.AddConsole())
                            .AddPayroc("Payroc"); //                                                    <---- Config hook
                    })
                    .Build();

    public static IHost HostSetupExample4()
        => Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services
                            .AddHttpClient()
                            .AddLogging(logging => logging.AddConsole())
                            .AddPayroc(options => //                                                  <---- Config hook
                            {
                                options.ApiKey = "test";
                                options.Environment = PayrocEnvironment.LocalEmulation;
                            });         
                    })
                    .Build();
}
