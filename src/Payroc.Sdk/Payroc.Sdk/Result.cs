using Payroc.Sdk.Errors;
using System.Diagnostics.CodeAnalysis;

namespace Payroc.Sdk;

public record Result
{
    private Result() { }

    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; init; }

    public IError? Error { get; init; }

    internal static Result Ok() => new() { IsSuccess = true };

    internal static Result Fail(IError failure) => new() { Error = failure };
}

public record Result<TSuccess> where TSuccess : class
{
    private Result() { }

    [MemberNotNullWhen(true, nameof(Content))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; init; }

    public TSuccess? Content { get; init; }

    public IError? Error { get; init; }

    internal static Result<TSuccess> Ok(TSuccess obj) => new() { Content = obj, IsSuccess = true };

    internal static Result<TSuccess> Fail(IError failure) => new() { Error = failure };
}
