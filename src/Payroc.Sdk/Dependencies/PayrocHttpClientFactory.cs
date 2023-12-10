namespace Payroc.Sdk.Dependencies;

// Wraps the handling of HttpClient so we can make DI and manual construction solutions interchangeable
internal partial class PayrocHttpClientFactory : IPayrocHttpClientFactory
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly HttpClient? _httpClient;

    public PayrocHttpClientFactory(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    protected PayrocHttpClientFactory(HttpClient httpClient) => _httpClient = httpClient;

    public HttpClient HttpClient => _httpClient ?? _httpClientFactory!.CreateClient(nameof(PayrocHttpClientFactory));

    public static PayrocHttpClientFactory Create(HttpClient httpClient) => new PayrocHttpClientFactory(httpClient);
}
