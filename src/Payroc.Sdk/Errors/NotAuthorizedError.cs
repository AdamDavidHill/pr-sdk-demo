namespace Payroc.Sdk.Errors;

public record NotAuthorizedError : IError
{
    public PayrocSdkError SdkError => PayrocSdkError.NotAuthorized;
    public string Type => "https://docs.payroc.com/api/errors#not-authorized";
    public string Title => "Not authorized";
    public string Details => "We can’t verify your identity. This can occur if your Bearer token has expired.";
    public ErrorItem[] Errors => Array.Empty<ErrorItem>();
}
