using Payroc.Sdk.Errors;

namespace Payroc.Sdk.Web;

internal partial class PayrocSession
{
    private IOAuthSessionState? _mutableState = null;

    internal async Task<Result> EnsureAuthenticated(CancellationToken cancellationToken)
    {
        if (_mutableState?.Valid ?? false)
        {
            return Result.Ok();
        }

        var auth = await _api.Authenticate(cancellationToken).ConfigureAwait(false);

        if (auth.IsSuccess)
        {
            _mutableState = auth.Content;

            return Result.Ok();
        }

        return Result.Fail(new NotAuthorizedError());
    }

    internal async Task<Result<T>> EnsureAuthenticated<T>(Task<Result<T>> task, CancellationToken cancellationToken) where T : class
    {
        if (_mutableState?.Valid ?? false)
        {
            return await task;
        }

        var auth = await _api.Authenticate(cancellationToken).ConfigureAwait(false);

        if (auth.IsSuccess)
        {
            _mutableState = auth.Content;

            return await task;
        }

        return Result<T>.Fail(new NotAuthorizedError());
    }
}
