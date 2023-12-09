namespace Payroc.Sdk.Errors;

public interface IError
{
    PayrocSdkError SdkError { get; }
    string Type { get; }
    string Title { get; }
    string Details { get; }
    ErrorItem[] Errors { get; }
}
