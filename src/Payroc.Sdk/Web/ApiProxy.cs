using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payroc.Sdk.Config;
using Payroc.Sdk.Dependencies;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy : IApiProxy
{
    private readonly IOptions<PayrocOptions> _config;
    private readonly IPayrocLoggerFactory _loggerFactory;
    private readonly IPayrocHttpClientFactory _httpClientFactory;
    private ILogger? _logger;

    public ApiProxy(IOptions<PayrocOptions> config, IPayrocLoggerFactory loggerFactory, IPayrocHttpClientFactory httpClientFactory)
        => (_config, _loggerFactory, _httpClientFactory)
        = (config, loggerFactory, httpClientFactory);

    // How we'd access the HttpClient in a class like this
    private HttpClient HttpClient => _httpClientFactory.HttpClient;

    // How we'd access the logger in a class like this, could 
    private ILogger Logger
    {
        get
        {
            _logger ??= _loggerFactory.CreateLogger(nameof(PayrocService));

            return _logger;
        }
    }

    private Task<Result<TResult>> CallApi<TResult>(HttpMethod method, string url, CancellationToken cancellationToken) where TResult : class
        => CallApi<TResult>(method, url, null, cancellationToken);

    private Task<Result<TResult>> CallApi<TResult>(HttpMethod method, string url, string? content, CancellationToken cancellationToken) where TResult : class
    {
        /*
         Dummy implementation, real one would deserialize / extract tokens
         Something like:
           var request = new HttpRequestMessage(HttpMethod.Post, url);
           request.Headers.Add("x-api-key", _config.Value.ApiKey);
           var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
           etc.

         This would parse any API errors, populating the Result.Error field accordingly.
         These errors would be expressed with both a convenience enum and also a typed Error, for the user's ease of switching/handling
         e.g.
           var result = Result<TResult>.Fail(new NotAuthorizedError());

         Exceptions would only be used in truly Exceptional circumstances.
         Logical errors would use `Error`s in `Result`s.
        */

        return Task.FromResult(Result<TResult>.Ok(default!));
    }

    private Task<Result> CallApi<T>(HttpMethod method, string url, string? content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        => CallApi(method, url, content, headers, cancellationToken);

    private Task<Result> CallApi(HttpMethod method, string url, CancellationToken cancellationToken)
        => CallApi(method, url, null, new() { }, cancellationToken);

    private Task<Result> CallApi(HttpMethod method, string url, string? content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        => Task.FromResult(Result.Ok());
}
