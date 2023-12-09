namespace Payroc.Sdk.Errors;

public record BadRequest : IError
{
    public BadRequest(ErrorItem[] errors) => Errors = errors;

    public PayrocSdkError SdkError => PayrocSdkError.BadRequest;
    public string Type => "https://docs.payroc.com/api/errors#bad-request";
    public string Title => "Bad request";
    public string Details => "One or more validation errors occurred, see error section for more info";
    public ErrorItem[] Errors { get; }
}
