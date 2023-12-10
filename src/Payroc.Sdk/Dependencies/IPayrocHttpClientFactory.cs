namespace Payroc.Sdk.Dependencies;

internal interface IPayrocHttpClientFactory
{
    HttpClient HttpClient { get; }
}
