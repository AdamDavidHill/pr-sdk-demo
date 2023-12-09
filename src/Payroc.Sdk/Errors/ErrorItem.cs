namespace Payroc.Sdk.Errors;

public record ErrorItem
{
    public string? Parameter { get; init; }
    public string? Detail { get; init; }
    public string? Message { get; init; }
}
