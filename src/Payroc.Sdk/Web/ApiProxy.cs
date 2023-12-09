using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payroc.Sdk.Config;

namespace Payroc.Sdk.Web;

internal partial class ApiProxy : IApiProxy
{
    private readonly ILogger<PayrocService>? _logger;
    private readonly IOptions<PayrocOptions> _config;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiProxy(ILogger<PayrocService>? logger, IOptions<PayrocOptions> config, IHttpClientFactory httpClientFactory)
        => (_logger, _config, _httpClientFactory)
        = (logger, config, httpClientFactory);

    private HttpClient HttpClient => _httpClientFactory.CreateClient(nameof(ApiProxy));

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
