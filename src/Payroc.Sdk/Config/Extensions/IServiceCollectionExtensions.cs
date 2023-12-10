using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Payroc.Sdk.Dependencies;
using Payroc.Sdk.Web;

namespace Payroc.Sdk.Config.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPayroc(this IServiceCollection services, IConfigurationSection payrocConfigSection)
        => services
            .AddScoped<IPayrocService, PayrocService>()
            .AddScoped<IApiProxy, ApiProxy>()
            .AddScoped<IPayrocLoggerFactory, PayrocLoggerFactory>()
            .AddScoped<IPayrocHttpClientFactory, PayrocHttpClientFactory>()        
            .AddSingleton<IValidateOptions<PayrocOptions>, PayrocConfigValidator>()
            .Configure<PayrocOptions>(payrocConfigSection)
            .AddSingleton(container => container.GetService<IOptions<PayrocOptions>>()!.Value); // Trigger an `OptionsValidationException` if config is bad / unset

    public static IServiceCollection AddPayroc(this IServiceCollection services, string configSectionPath = "Payroc")
        => services
            .AddScoped<IPayrocService, PayrocService>()
            .AddScoped<IApiProxy, ApiProxy>()
            .AddScoped<IPayrocLoggerFactory, PayrocLoggerFactory>()
            .AddScoped<IPayrocHttpClientFactory, PayrocHttpClientFactory>()
            .AddSingleton<IValidateOptions<PayrocOptions>, PayrocConfigValidator>()
            .AddOptions<PayrocOptions>()
            .BindConfiguration(configSectionPath)
            .ValidateOnStart()
            .Services;

    public static IServiceCollection AddPayroc(this IServiceCollection services, Action<PayrocOptions> configureOptions)
        => services
            .AddScoped<IPayrocService, PayrocService>()
            .AddScoped<IApiProxy, ApiProxy>()
            .AddScoped<IPayrocLoggerFactory, PayrocLoggerFactory>()
            .AddScoped<IPayrocHttpClientFactory, PayrocHttpClientFactory>()
            .AddSingleton<IValidateOptions<PayrocOptions>, PayrocConfigValidator>()
            .Configure(configureOptions)
            .AddSingleton(container => container.GetService<IOptions<PayrocOptions>>()!.Value); // Trigger an `OptionsValidationException` if config is bad / unset

    public static IServiceCollection AddPayroc(this IServiceCollection services, PayrocOptions options)
        => services
            .AddScoped<IPayrocService, PayrocService>()
            .AddScoped<IApiProxy, ApiProxy>()
            .AddScoped<IPayrocLoggerFactory, PayrocLoggerFactory>()
            .AddScoped<IPayrocHttpClientFactory, PayrocHttpClientFactory>()
            .AddSingleton<IValidateOptions<PayrocOptions>, PayrocConfigValidator>()
            .AddOptions<PayrocOptions>()
            .Configure(o =>
                {
                    o.ApiKey = options.ApiKey;
                    o.Environment = options.Environment;
                })
            .ValidateOnStart()
            .Services;
}
