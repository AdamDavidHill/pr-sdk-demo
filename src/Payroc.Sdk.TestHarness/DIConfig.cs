using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payroc.Sdk.Config;
using Payroc.Sdk.Config.Extensions;

namespace Payroc.Sdk.TestHarness;

internal class DIConfig
{
    public static (int Index, IHost Host)[] Examples
        => new (int, IHost)[]
        {
            (1, HostSetup1()),
            (2, HostSetup2()),
            (3, HostSetup3()),
            (4, HostSetup4())
        };

    public static IHost HostSetup1()
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

    public static IHost HostSetup2()
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

    public static IHost HostSetup3()
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

    public static IHost HostSetup4()
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
